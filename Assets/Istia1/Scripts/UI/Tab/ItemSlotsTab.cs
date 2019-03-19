using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.UI.Tab
{
    public class ItemSlotsTab : TabBase
    {
        [Title("State")]
        [OdinSerialize] public List<Slot.ItemSlot> Contents { get; set; }

        [Title("Editor Tools")]
        [OdinSerialize] Transform SlotsRoot { get; set; }
        
        [Button("Search Slots")]
        void SearchSlots()
        {
            Contents = new List<Slot.ItemSlot>();
            foreach(Transform slot in SlotsRoot)
            {
                Contents.Add(slot.GetComponent<Slot.ItemSlot>());
            }
        }

        [Button("Reset Slot IDs")]
        void ResetSlotIDs()
        {
            for (int id = 0; id < Contents.Count; id++) Contents[id].SlotID = id;
        }
    }
}