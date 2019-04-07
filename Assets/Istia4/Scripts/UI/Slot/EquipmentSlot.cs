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
    public class EquipmentSlot : InventorySlotBase, Save.ISavable, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
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

            if (UnityEngine.Input.GetMouseButton(Config.KeyConfig.DisposeItemAllMouseButton) && UnityEngine.Input.GetKey(Config.KeyConfig.DisposeItemAllKey))
            {
                if (!EquipmentInfo.Droppable)
                {
                    Debug.Log("この装備品は捨てられません。");
                    return;
                }
                DropAll();
            }
            else if (UnityEngine.Input.GetMouseButton(Config.KeyConfig.EquipMouseButton))
            {
                if (!EquipHandler.Equip(EquipmentInfo)) return;
                Emptimize();
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            PressOverlay.gameObject.SetActive(false);
        }

        void Save.ISavable.Save()
        {
            Save.SaveHandler.Save(this, m_EquipmentInfoID, nameof(m_EquipmentInfoID));
        }

        void Save.ISavable.Load()
        {
            Save.SaveHandler.Load(this, ref m_EquipmentInfoID, nameof(m_EquipmentInfoID));
        }

        [Title("Required")]
        [OdinSerialize, Required] DB.Inventory.EquipmentInfoProvider EquipmentInfoProvider { get; set; }
        [OdinSerialize, Required] GameSystem.Actor.Player.InventoryHandler InventoryHandler { get; set; }
        [OdinSerialize, Required] GameSystem.Actor.Player.EquipHandler EquipHandler { get; set; }
        [OdinSerialize, Required] GameSystem.Prop.DropHandler DropHandler { get; set; }

        [TitleGroup("Content")]
        [OdinSerialize, ReadOnly] string m_EquipmentInfoID;
        public string EquipmentInfoID
        {
            get { return m_EquipmentInfoID; }
        }

        [TitleGroup("Content")]
        [OdinSerialize, ReadOnly] public DB.Inventory.EquipmentInfo EquipmentInfo { get; private set; }

        [Title("UI Reference")]
        [OdinSerialize] Image IconImage { get; set; }
        [OdinSerialize] Image HoverOverlay { get; set; }
        [OdinSerialize] Image PressOverlay { get; set; }

        #region Overrides

        public override bool IsEmpty()
        {
            return EquipmentInfo == null;
        }

        public override void Emptimize()
        {
            m_EquipmentInfoID = null;
            EquipmentInfo = null;
            IconImage.sprite = null;
            IconImage.gameObject.SetActive(false);
            HoverOverlay.gameObject.SetActive(false);
            PressOverlay.gameObject.SetActive(false);
        }

        public override void Dispose()
        {
            if (IsEmpty()) throw new System.Exception("スロットは既に空であるため、装備品を破棄できません。");
            Emptimize();
        }

        public override void DisposeAll()
        {
            // 装備品スロットは、アイテムスロットと異なり最大1個までしか格納できないので、DisposeAllもDisposeと同一です。
            Dispose();
        }

        public override void Drop()
        {
            if (IsEmpty()) throw new System.Exception("スロットは既に空であるため、装備品をドロップできません。。");
            DropHandler.Drop(InventoryHandler.transform.position, EquipmentInfo);
            Emptimize();
        }

        public override void DropAll()
        {
            // 装備品スロットは、アイテムスロットと異なり最大1個までしか格納できないので、DisposeAllもDisposeと同一です。
            Drop();
        }

        public override bool Push(DB.Inventory.InventoryItemInfoBase inventoryItemInfo)
        {
            if (!IsEmpty()) return false;
            if (!(inventoryItemInfo is DB.Inventory.EquipmentInfo)) return false;

            var equipmentInfo = inventoryItemInfo as DB.Inventory.EquipmentInfo;
            m_EquipmentInfoID = equipmentInfo.ID;
            EquipmentInfo = equipmentInfo;
            IconImage.sprite = equipmentInfo.IconSprite;
            IconImage.gameObject.SetActive(true);
            return true;
        }

        public override void Refresh()
        {
            Emptimize();
            if (string.IsNullOrEmpty(EquipmentInfoID)) return;
            Push(EquipmentInfoProvider.Provide(EquipmentInfoID));
        }

        #endregion
    }
}