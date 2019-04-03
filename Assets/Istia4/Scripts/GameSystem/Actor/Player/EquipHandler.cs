﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using ISavable = EllGames.Istia4.Save.ISavable;

namespace EllGames.Istia4.GameSystem.Actor.Player
{
    public class EquipHandler : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] UI.Window.EquipmentWindow EquipWindow { get; set; }
        [OdinSerialize, Required] InventoryHandler InventoryHandler { get; set; }

        public List<DB.Inventory.EquipmentInfo> Equipments()
        {
            var equipments = new List<DB.Inventory.EquipmentInfo>();
            foreach(var slot in EquipWindow.EquipSlots)
            {
                if (!slot.IsEmpty()) equipments.Add(slot.EquipmentInfo);
            }
            return equipments;
        }

        /// <summary>
        /// 対象を装備します。
        /// </summary>
        /// <param name="equipmentInfo"></param>
        /// <returns></returns>
        [Button("Equip")]
        public bool Equip(DB.Inventory.EquipmentInfo equipmentInfo)
        {
            foreach(var slot in EquipWindow.EquipSlots)
            {
                if (slot.Equip(equipmentInfo)) return true;
            }

            return false;
        }

        public bool Equip(int slotID, DB.Inventory.EquipmentInfo equipmentInfo)
        {
            return EquipWindow.SearchSlot(slotID).Equip(equipmentInfo);
        }

        /// <summary>
        /// 装備解除します。
        /// </summary>
        /// <param name="slotID"></param>
        /// <returns></returns>
        [Button("Unequip")]
        public bool Unequip(int slotID)
        {
            if (!InventoryHandler.Push(EquipWindow.SearchSlot(slotID).EquipmentInfo)) return false;
            return EquipWindow.SearchSlot(slotID).Unequip();
        }

        /// <summary>
        /// すべての装備品を装備解除します。
        /// </summary>
        [Button("Unequip All")]
        public void UnequipAll()
        {
            EquipWindow.EquipSlots.ForEach(slot => slot.Emptimize());
        }

        /// <summary>
        /// 装備品の情報をセーブします。
        /// </summary>
        [Button("Save")]
        public void Save()
        {
            EquipWindow.EquipSlots.ForEach(slot => ((ISavable)slot).Save());
        }

        /// <summary>
        /// 装備品の情報をロードします。
        /// </summary>
        [Button("Load")]
        public void Load()
        {
            EquipWindow.EquipSlots.ForEach(slot => ((ISavable)slot).Load());
        }

        /// <summary>
        /// 装備品の情報をUIに反映します。
        /// </summary>
        [Button("Refresh")]
        public void Refresh()
        {
            EquipWindow.EquipSlots.ForEach(slot => slot.Refresh());
        }
    }
}