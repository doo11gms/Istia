using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia3.UI.Window
{
    public class EquipmentWindow : WindowBase
    {
        [OdinSerialize] public List<Slot.EquipmentSlot> EquipmentSlots { get; private set; } = new List<Slot.EquipmentSlot>();

        public Slot.EquipmentSlot SearchSlot(int slotID)
        {
            foreach(var slot in EquipmentSlots)
            {
                if (slot.SlotID == slotID) return slot;
            }

            return null;
        }
    }
}