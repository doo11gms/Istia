using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.UI.Tab
{
    public class SlotsTab : TabBase
    {
        [OdinSerialize] public List<Slot.SlotBase> Slots { get; set; } = new List<Slot.SlotBase>();

        [Title("Editor Tools Settings")]
        [OdinSerialize] Transform SlotsRoot { get; set; }

        [Title("Buttons")]
        [Button("Find Slots")]
        void FindSlots()
        {
            Slots = new List<Slot.SlotBase>();
            foreach(Transform child in SlotsRoot)
            {
                var slot = child.GetComponent<Slot.SlotBase>();
                if (slot == null) continue;
                Slots.Add(slot);
            }
        }

        [Button("Reset Slot IDs")]
        void ResetSlotIDs()
        {
            for(int id = 0; id < Slots.Count; id++)
            {
                Slots[id].SlotID = id;
            }
        }
    }
}