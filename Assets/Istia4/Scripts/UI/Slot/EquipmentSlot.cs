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
                if (!EquipmentInfo.Disposable)
                {
                    Debug.Log("この装備品は捨てられません。");
                    return;
                }
                Emptimize();
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
        [OdinSerialize, Required] public DB.Inventory.EquipmentInfoProvider EquipmentInfoProvider { get; private set; }
        [OdinSerialize, Required] public GameSystem.Actor.Player.InventoryHandler InventoryHandler { get; private set; }
        [OdinSerialize, Required] public GameSystem.Actor.Player.EquipHandler EquipHandler { get; private set; }

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

        public void Emptimize()
        {
            m_EquipmentInfoID = null;
            EquipmentInfo = null;
            IconImage.sprite = null;
            IconImage.gameObject.SetActive(false);
            HoverOverlay.gameObject.SetActive(false);
            PressOverlay.gameObject.SetActive(false);
        }

        public bool IsEmpty()
        {
            return EquipmentInfo == null;
        }

        public bool Push(DB.Inventory.EquipmentInfo equipmentInfo)
        {
            if (!IsEmpty()) return false;

            m_EquipmentInfoID = equipmentInfo.ID;
            EquipmentInfo = equipmentInfo;
            IconImage.sprite = equipmentInfo.IconSprite;
            IconImage.gameObject.SetActive(true);
            return true;
        }

        public bool Dispose()
        {
            if (IsEmpty()) return false;

            Emptimize();
            return true;
        }

        public void Refresh()
        {
            Emptimize();

            if (string.IsNullOrEmpty(EquipmentInfoID)) return;

            var info = EquipmentInfoProvider.Provide(EquipmentInfoID);
            if (info == null) throw new System.Exception("EquipmentInfoIDに対応するEquipmentInfoが見つかりません。");

            Push(info);
        }
    }
}