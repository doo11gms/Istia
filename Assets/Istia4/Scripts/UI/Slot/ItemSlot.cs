﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.UI.Slot
{
    public class ItemSlot : InventorySlotBase, Save.ISavable, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (IsEmpty()) return;
            HoverOverlay.gameObject.SetActive(true);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            HoverOverlay.gameObject.SetActive(false);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (IsEmpty()) return;

            PressOverlay.gameObject.SetActive(true);

            if (UnityEngine.Input.GetMouseButton(Config.KeyConfig.UseItemMouseButton))
            {
                Use();
                return;
            }

            if (UnityEngine.Input.GetMouseButton(Config.KeyConfig.DisposeItemAllMouseButton) && UnityEngine.Input.GetKey(Config.KeyConfig.DisposeItemAllKey))
            {
                if (ItemInfo.Droppable)
                {
                    DropAll();
                }
                else
                {
                    Debug.Log("このアイテムはドロップできません。");
                }
                return;
            }

            if (UnityEngine.Input.GetMouseButton(1))
            {
                if (!ItemInfo.Usable) return;
                ShortcutInfoContainer.Assign(new GameSystem.Shortcut.ShortcutInfo(ItemInfo.IconSprite, ItemInfo.ID, GameSystem.Shortcut.SHORTCUT_TYPE.UseItemShortcut));
                ShortcutInfoContainer.gameObject.SetActive(true);
                return;
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            PressOverlay.gameObject.SetActive(false);
        }

        void Save.ISavable.Save()
        {
            Save.SaveHandler.Save(this, m_ItemInfoID, nameof(m_ItemInfoID));
            Save.SaveHandler.Save(this, m_Count, nameof(m_Count));
        }

        void Save.ISavable.Load()
        {
            Save.SaveHandler.Load(this, ref m_ItemInfoID, nameof(m_ItemInfoID));
            Save.SaveHandler.Load(this, ref m_Count, nameof(m_Count));
        }

        [Title("Required")]
        [OdinSerialize, Required] DB.Inventory.ItemInfoProvider ItemInfoProvider { get; set; }
        [OdinSerialize, Required] GameSystem.Actor.Player.InventoryHandler InventoryHandler { get; set; }
        [OdinSerialize, Required] GameSystem.Item.ItemCoolDownHandler ItemCoolDownHandler { get; set; }
        [OdinSerialize, Required] GameSystem.Prop.DropHandler DropHandler { get; set; }

        [TitleGroup("Meta")]
        [OdinSerialize] public DB.Inventory.ItemCategory ItemCategory { get; private set; }

        [TitleGroup("Content")]
        [OdinSerialize, ReadOnly] string m_ItemInfoID;
        public string ItemInfoID
        {
            get { return m_ItemInfoID; }
        }

        [TitleGroup("Content")]
        [OdinSerialize, ReadOnly] public DB.Inventory.ItemInfo ItemInfo { get; private set; }

        [TitleGroup("Content")]
        [OdinSerialize, ReadOnly] int m_Count;
        public int Count
        {
            get { return m_Count; }
        }

        [Title("UI Reference")]
        [OdinSerialize] Container.ShortcutInfoContainer ShortcutInfoContainer { get; set; }
        [OdinSerialize] Image IconImage { get; set; }
        [OdinSerialize] Image CoolDownOverlay { get; set; }
        [OdinSerialize] Image HoverOverlay { get; set; }
        [OdinSerialize] Image PressOverlay { get; set; }
        [OdinSerialize] Text CountText { get; set; }

        #region Overrides

        public override bool IsEmpty()
        {
            return ItemInfo == null;
        }

        public override void Emptimize()
        {
            m_ItemInfoID = null;
            ItemInfo = null;
            IconImage.sprite = null;
            IconImage.gameObject.SetActive(false);
            CoolDownOverlay.fillAmount = 0f;
            CoolDownOverlay.gameObject.SetActive(false);
            HoverOverlay.gameObject.SetActive(false);
            PressOverlay.gameObject.SetActive(false);
            SetCount(0);
        }

        public override void Dispose()
        {
            if (IsEmpty()) throw new System.Exception("スロットは既に空であるため、アイテムを破棄できません。");
            SetCount(Count - 1);
            if (Count == 0) Emptimize();
        }

        public override void DisposeAll()
        {
            if (IsEmpty()) throw new System.Exception("スロットは既に空であるため、アイテムを破棄できません。");
            Emptimize();
        }

        public override void Drop()
        {
            if (IsEmpty()) throw new System.Exception("スロットは既に空であるため、アイテムをドロップできません。");
            DropHandler.Drop(InventoryHandler.transform.position, ItemInfo);
            SetCount(Count - 1);
            if (Count == 0) Emptimize();
        }

        public override void DropAll()
        {
            if (IsEmpty()) throw new System.Exception("スロットは既に空であるため、アイテムをドロップできません。");
            DropHandler.Drop(InventoryHandler.transform.position, ItemInfo, Count);
            Emptimize();
        }

        public override bool Push(DB.Inventory.InventoryItemInfoBase inventoryItemInfo)
        {
            if (!(inventoryItemInfo is DB.Inventory.ItemInfo)) return false;

            var itemInfo = inventoryItemInfo as DB.Inventory.ItemInfo;
            if (itemInfo.ItemCategory != ItemCategory) return false;
            if (Count + 1 > itemInfo.MaxStackCount) return false;
            if (!IsEmpty() && itemInfo.ID != ItemInfo.ID) return false;

            m_ItemInfoID = itemInfo.ID;
            ItemInfo = itemInfo;
            IconImage.sprite = itemInfo.IconSprite;
            IconImage.gameObject.SetActive(true);
            SetCount(Count + 1);

            return true;
        }

        public override void Refresh()
        {
            if (string.IsNullOrEmpty(ItemInfoID))
            {
                Emptimize();
                return;
            }

            var info = ItemInfoProvider.Provide(ItemInfoID);
            var cnt = Count;

            Emptimize();

            Push(info);
            SetCount(cnt);
        }

        #endregion

        void SetCount(int count)
        {
            m_Count = count;
            CountText.text = count.ToString();
            CountText.gameObject.SetActive(count > 1);
        }

        public bool Use()
        {
            if (IsEmpty()) throw new System.Exception("スロットが空であるため、使用できません。");
            if (!ItemInfo.Usable) throw new System.Exception("使用できないアイテムです。");

            if (ItemInfo.UsingCoolTime)
            {
                if (!ItemCoolDownHandler.CoolTimeFinished(ItemInfo))
                {
                    Debug.Log("クールタイム中のため、使用できません。");
                    return false;
                }

                ItemCoolDownHandler.Assign(ItemInfo);
            }

            if (ItemInfo.ItemUsingEffects != null)
            {
                foreach(var effect in ItemInfo.ItemUsingEffects)
                {
                    Instantiate(effect);
                }
            }

            Dispose();
            return true;
        }

        private void Update()
        {
            if (IsEmpty()) return;
            if (ItemInfo.UsingCoolTime)
            {
                CoolDownOverlay.fillAmount = ItemCoolDownHandler.CoolTimeRemain(ItemInfo) / ItemInfo.CoolTime;
                CoolDownOverlay.gameObject.SetActive(!ItemCoolDownHandler.CoolTimeFinished(ItemInfo));
            }
        }
    }
}