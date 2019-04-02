using System.Collections;
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
    public class ItemSlot : InventorySlotBase
    {
        [Title("Required")]
        [OdinSerialize, Required] public DB.Inventory.ItemInfoProvider ItemInfoProvider { get; private set; }
        [OdinSerialize, Required] public GameSystem.Actor.Player.InventoryHandler InventoryHandler { get; private set; }

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
        [OdinSerialize, ReadOnly] public int Count { get; private set; }

        [Title("UI Reference")]
        [OdinSerialize] Image IconImage { get; set; }
        [OdinSerialize] Image CoolDownOverlay { get; set; }
        [OdinSerialize] Image HoverOverlay { get; set; }
        [OdinSerialize] Image PressOverlay { get; set; }
        [OdinSerialize] Text CountText { get; set; }

        void SetCount(int count)
        {
            Count = count;
            CountText.text = count.ToString();
        }

        [Button("Emptimize")]
        public void Emptimize()
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
            CountText.gameObject.SetActive(false);
        }

        public bool IsEmpty()
        {
            return ItemInfo == null;
        }

        [Button("Push")]
        public bool Push(DB.Inventory.ItemInfo itemInfo)
        {
            if (itemInfo.ItemCategory != ItemCategory) return false;
            if (Count + 1 > itemInfo.MaxStackCount) return false;

            m_ItemInfoID = itemInfo.ID;
            ItemInfo = itemInfo;
            IconImage.sprite = itemInfo.IconSprite;
            IconImage.gameObject.SetActive(true);
            SetCount(Count + 1);
            if (Count > 1) CountText.gameObject.SetActive(true);

            return true;
        }

        [Button("Dispose")]
        public bool Dispose()
        {
            if (IsEmpty()) return false;

            SetCount(Count - 1);

            if (Count == 0)
            {
                Emptimize();
            }
            else if (Count == 1)
            {
                CountText.gameObject.SetActive(false);
            }

            return true;
        }
    }
}