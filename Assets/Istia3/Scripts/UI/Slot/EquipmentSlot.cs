using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia3.UI.Slot
{
    public class EquipmentSlot : InventoryItemSlotBase, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        enum WINDOW_TYPE
        {
            InventoryWindow,
            EquipmentWindow
        }

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
            if (Input.GetMouseButton(Config.KeyConfig.EquipMouseButton))
            {
                switch(WindowType)
                {
                    case WINDOW_TYPE.InventoryWindow:
                        break;
                    case WINDOW_TYPE.EquipmentWindow:
                        EquipmentHandler.Unequip(SlotID);
                        break;
                }
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            PressOverlay.gameObject.SetActive(false);
        }

        [Title("Required")]
        [OdinSerialize, Required] GameSystem.Actor.Player.Equipment.EquipmentHandler EquipmentHandler { get; set; }

        [Title("Settings")]
        [OdinSerialize] WINDOW_TYPE WindowType { get; set; } = WINDOW_TYPE.InventoryWindow;

        [Title("State")]
        [OdinSerialize, ReadOnly] DB.Inventory.EquipmentInfo Content { get; set; }

        [Title("UI Reference")]
        [OdinSerialize] Image IconImage { get; set; }
        [OdinSerialize] Image HoverOverlay { get; set; }
        [OdinSerialize] Image PressOverlay { get; set; }

        public bool IsEmpty() => Content == null;

        public void Assign(DB.Inventory.EquipmentInfo equipmentInfo)
        {
            Content = equipmentInfo;
            IconImage.sprite = equipmentInfo.IconSprite;
            IconImage.gameObject.SetActive(true);
        }

        public void Unassign()
        {
            Content = null;
            IconImage.sprite = null;
            IconImage.gameObject.SetActive(false);
            HoverOverlay.gameObject.SetActive(false);
            PressOverlay.gameObject.SetActive(false);
        }
    }
}