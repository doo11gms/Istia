using System.Collections;
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
        [OdinSerialize] UI.Window.EquipmentWindow EquipWindow { get; set; }

        [Button("Equip")]
        public bool Equip(int slotID, DB.Inventory.EquipmentInfo equipmentInfo)
        {
            return EquipWindow.SearchSlot(slotID).Equip(equipmentInfo);
        }

        [Button("Unequip")]
        public bool Unequip(int slotID)
        {
            return EquipWindow.SearchSlot(slotID).Unequip();
        }

        [Button("Unequip All")]
        public void UnequipAll()
        {
            EquipWindow.EquipSlots.ForEach(slot => slot.Emptimize());
        }

        [Button("Refresh")]
        public void Refresh()
        {
            EquipWindow.EquipSlots.ForEach(slot => slot.Refresh());
        }

        [Button("Save")]
        public void Save()
        {
            EquipWindow.EquipSlots.ForEach(slot => ((ISavable)slot).Save());
        }

        [Button("Load")]
        public void Load()
        {
            EquipWindow.EquipSlots.ForEach(slot => ((ISavable)slot).Load());
        }
    }
}