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
    public class InventoryWindow : WindowBase
    {
        [OdinSerialize] public List<Slot.ItemSlot> ItemSlots { get; set; } = new List<Slot.ItemSlot>();

        public Slot.ItemSlot SearchSlot(int slotID)
        {
            foreach (var slot in ItemSlots)
            {
                if (slot.SlotID == slotID) return slot;
            }

            return null;
        }
    }
}