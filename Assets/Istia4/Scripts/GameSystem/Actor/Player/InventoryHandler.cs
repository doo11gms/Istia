using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.GameSystem.Actor.Player
{
    public class InventoryHandler : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] UI.Window.InventoryWindow InventoryWindow;

        int GetTabID(DB.Inventory.ItemInfo itemInfo)
        {
            if (itemInfo.ItemCategory.ID == "消耗品") return 0;

            throw new System.Exception("該当するタブが見つかりません。");
        }

        [Button("Push")]
        public bool Push(DB.Inventory.ItemInfo itemInfo)
        {
            foreach(var slot in InventoryWindow.SearchTab(GetTabID(itemInfo)).Slots)
            {
                if ((slot as UI.Slot.ItemSlot).Push(itemInfo)) return true;
            }

            return false;
        }

        public bool Push(DB.Inventory.ItemInfo itemInfo, int tabID, int slotID)
        {
            var slot = InventoryWindow.SearchSlot(tabID, slotID);
            if (slot == null) throw new System.Exception("対象のスロットが見つかりません。");
            return slot.Push(itemInfo);
        }

        [Button("Dispose")]
        public bool Dispose(int tabID, int slotID)
        {
            var slot = InventoryWindow.SearchSlot(tabID, slotID);
            if (slot == null) throw new System.Exception("対象のスロットが見つかりません。");
            return slot.Dispose();
        }

        public bool DisposeAll(UI.Slot.ItemSlot itemSlot)
        {
            return itemSlot.DisposeAll();
        }

        [Button("Emptimize")]
        public void Emptimize()
        {
            foreach(var tab in InventoryWindow.Tabs)
            {
                foreach(var slot in tab.Slots)
                {
                    (slot as UI.Slot.ItemSlot).Emptimize();
                }
            }
        }

        public void Emptimize(int tabID, int slotID)
        {
            var slot = InventoryWindow.SearchSlot(tabID, slotID);
            if (slot == null) throw new System.Exception("対象のスロットが見つかりません。");
            slot.Emptimize();
        }

        [Button("Use")]
        public bool Use(UI.Slot.ItemSlot itemSlot)
        {
            return itemSlot.Use();
        }

        [Button("Sort")]
        public void Sort()
        {
            Debug.Log("TODO");
        }
    }
}