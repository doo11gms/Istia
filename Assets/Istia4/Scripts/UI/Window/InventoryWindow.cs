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
        [OdinSerialize] public List<UI.Tab.SlotsTab> Tabs { get; private set; } = new List<UI.Tab.SlotsTab>();

        // XXX: SearchSlotによって返る値はEquipmentSlotの可能性がある。

        public UI.Slot.ItemSlot SearchSlot(int tabID, int slotID)
        {
            foreach (var tab in Tabs)
            {
                if (tab.TabID != tabID) continue;
                foreach (var slot in tab.Slots)
                {
                    if (slot.SlotID == slotID) return slot as UI.Slot.ItemSlot;
                }
            }

            return null;
        }

        public Tab.SlotsTab SearchTab(int tabID)
        {
            foreach(var tab in Tabs)
            {
                if (tab.TabID == tabID) return tab;
            }

            return null;
        }
    }
}