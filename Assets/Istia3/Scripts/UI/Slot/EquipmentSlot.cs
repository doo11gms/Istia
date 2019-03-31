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
    public class EquipmentSlot : InventoryItemSlotBase
    {
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