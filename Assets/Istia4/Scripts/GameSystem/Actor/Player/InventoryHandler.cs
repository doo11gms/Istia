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
        // TODO: ItemInfo,Equipinfoを受け取るんじゃなくてInventoryItemBaseをキャストして使って下さい
        // TODO:manage getTabID method

        [Title("Required")]
        [OdinSerialize, Required] UI.Window.InventoryWindow InventoryWindow;
        [OdinSerialize, Required] EquipHandler EquipHandler;

        int GetTabID(DB.Inventory.InventoryItemInfoBase inventoryItemInfo)
        {
            if (inventoryItemInfo is DB.Inventory.ItemInfo)
            {
                var info = inventoryItemInfo as DB.Inventory.ItemInfo;
                if (info.ItemCategory.ID == "消耗品") return 0;
            }
            else if (inventoryItemInfo is DB.Inventory.EquipmentInfo)
            {
                return 1;
            }

            throw new System.Exception("該当するタブが見つかりません。");
        }

        [Button("Push")]
        public bool Push(DB.Inventory.InventoryItemInfoBase inventoryItemInfo)
        {
            if (inventoryItemInfo is DB.Inventory.ItemInfo)
            {
                foreach (var slot in InventoryWindow.SearchTab(GetTabID(inventoryItemInfo)).Slots)
                {
                    if ((slot as UI.Slot.ItemSlot).Push(inventoryItemInfo as DB.Inventory.ItemInfo)) return true;
                }
            }
            else if (inventoryItemInfo is DB.Inventory.EquipmentInfo)
            {
                foreach (var slot in InventoryWindow.SearchTab(GetTabID(inventoryItemInfo)).Slots)
                {
                    if ((slot as UI.Slot.EquipmentSlot).Push(inventoryItemInfo as DB.Inventory.EquipmentInfo)) return true;
                }
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

        [Button("Save")]
        public void Save()
        {
            InventoryWindow.Tabs.ForEach(tab => tab.Slots.ForEach(slot => ((Save.ISavable)slot).Save()));
        }

        [Button("Load")]
        public void Load()
        {
            InventoryWindow.Tabs.ForEach(tab => tab.Slots.ForEach(slot => ((Save.ISavable)slot).Load()));
        }

        [Button("Refresh")]
        public void Refresh()
        {
            InventoryWindow.Tabs.ForEach(tab => tab.Slots.ForEach(slot => (slot as UI.Slot.ItemSlot).Refresh()));
        }
    }
}