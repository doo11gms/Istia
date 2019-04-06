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
    public class EquipSlot : SlotBase, Save.ISavable, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
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
            if (UnityEngine.Input.GetMouseButton(Config.KeyConfig.EquipMouseButton))
            {
                EquipHandler.Unequip(SlotID);
                return;
            }
            else
            {
                PressOverlay.gameObject.SetActive(true);
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
        [OdinSerialize, Required] public GameSystem.Actor.Player.EquipHandler EquipHandler { get; private set; }

        [TitleGroup("Meta")]
        [OdinSerialize] public DB.Inventory.EquipmentCategory EquipmentCategory { get; private set; }

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
        [OdinSerialize] Text EquipText { get; set; }

        public void Emptimize()
        {
            m_EquipmentInfoID = null;
            EquipmentInfo = null;
            IconImage.sprite = null;
            IconImage.gameObject.SetActive(false);
            EquipText.gameObject.SetActive(false);
            HoverOverlay.gameObject.SetActive(false);
            PressOverlay.gameObject.SetActive(false);
        }

        public bool Equip(DB.Inventory.EquipmentInfo equipmentInfo)
        {
            if (!IsEmpty())
            {
                Debug.Log("このスロットには既に別の装備品が装備されています。");
                return false;
            }

            if (equipmentInfo.EquipmentCategory != EquipmentCategory)
            {
                Debug.Log("カテゴリが異なるため、このスロットには装備できません。");
                return false;
            };

            m_EquipmentInfoID = equipmentInfo.ID;
            EquipmentInfo = equipmentInfo;
            IconImage.sprite = equipmentInfo.IconSprite;
            IconImage.gameObject.SetActive(true);
            EquipText.gameObject.SetActive(true);

            return true;
        }

        public DB.Inventory.EquipmentInfo Unequip()
        {
            if (IsEmpty()) throw new System.Exception("スロットが空であるため、装備解除に失敗しました。");

            var buf = EquipmentInfo;
            Emptimize();
            return buf;
        }

        public bool IsEmpty()
        {
            return EquipmentInfo == null;
        }

        public void Refresh()
        {
            if (string.IsNullOrEmpty(m_EquipmentInfoID))
            {
                Emptimize();
                return;
            }

            var info = EquipmentInfoProvider.Provide(m_EquipmentInfoID);
            if (info == null) throw new System.Exception("EquipmentInfoの取得に失敗しました。");

            Emptimize();
            if (!Equip(info)) throw new System.Exception("取得したEquipmentInfoを装備できません。");
        }
    }
}