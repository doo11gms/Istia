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
    public class InventoryHandler : SerializedMonoBehaviour, Save.ISavable
    {
        void Save.ISavable.Save()
        {
            InventoryWindow.Tabs.ForEach(tab => tab.Slots.ForEach(slot => ((Save.ISavable)slot).Save()));
        }

        void Save.ISavable.Load()
        {
            InventoryWindow.Tabs.ForEach(tab => tab.Slots.ForEach(slot => ((Save.ISavable)slot).Load()));
            Refresh();
        }

        [Title("Required")]
        [OdinSerialize, Required] UI.Window.InventoryWindow InventoryWindow;
        [OdinSerialize, Required] EquipHandler EquipHandler;
        [OdinSerialize, Required] Prop.DropHandler DropHandler;

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

        [Title("Buttons")]
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

        [Button("Emptimize")]
        public void Emptimize()
        {
            foreach (var tab in InventoryWindow.Tabs)
            {
                foreach (var slot in tab.Slots)
                {
                    if (slot is UI.Slot.ItemSlot) (slot as UI.Slot.ItemSlot).Emptimize();
                    if (slot is UI.Slot.EquipmentSlot) (slot as UI.Slot.EquipmentSlot).Emptimize();
                }
            }
        }

        [Button("Sort")]
        public void Sort()
        {
            Debug.Log("TODO");
        }

        public bool FindAndUse(DB.Inventory.ItemInfo itemInfo)
        {
            foreach(var slot in InventoryWindow.SearchTab(GetTabID(itemInfo)).Slots)
            {
                var itemSlot = slot as UI.Slot.ItemSlot;
                if (itemSlot.ItemInfoID == itemInfo.ID)
                {
                    return itemSlot.Use();
                }
            }
            Debug.Log("対象のアイテムがインベントリに存在しません。");
            return false;
        }

        [Button("Refresh")]
        public void Refresh()
        {
            InventoryWindow.Tabs.ForEach(tab => tab.Slots.ForEach(slot =>
            {
                if (slot is UI.Slot.ItemSlot) (slot as UI.Slot.ItemSlot).Refresh();
                if (slot is UI.Slot.EquipmentSlot) (slot as UI.Slot.EquipmentSlot).Refresh();
            }));
        }
    }
}