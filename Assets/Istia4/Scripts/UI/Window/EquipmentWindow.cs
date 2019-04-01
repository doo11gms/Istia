using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.UI.Window
{
    public class EquipmentWindow : WindowBase
    {
        [OdinSerialize] public List<Slot.EquipSlot> EquipSlots { get; set; } = new List<Slot.EquipSlot>();

        public Slot.EquipSlot SearchSlot(int slotID)
        {
            foreach(var slot in EquipSlots)
            {
                if (slot.SlotID == slotID) return slot;
            }

            return null;
        }
    }
}