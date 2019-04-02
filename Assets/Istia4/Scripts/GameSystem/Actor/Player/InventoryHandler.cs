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

        [Title("Required")]
        [OdinSerialize, Required] UI.Window.InventoryWindow InventoryWindow;
        [OdinSerialize, Required] EquipHandler EquipHandler;

        int GetTabID(DB.Inventory.ItemInfo itemInfo)
        {
            if (itemInfo.ItemCategory.ID == "消耗品") return 0;

            throw new System.Exception("該当するタブが見つかりません。");
        }

        int GetTabID(DB.Inventory.EquipmentInfo equipmentInfo)
        {
            return 1;
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

        [Button("PushEquipment")]
        public bool PushEquipment(DB.Inventory.EquipmentInfo equipmentInfo)
        {
            foreach(var slot in InventoryWindow.SearchTab(GetTabID(equipmentInfo)).Slots)
            {
                if ((slot as UI.Slot.EquipmentSlot).Push(equipmentInfo)) return true;
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