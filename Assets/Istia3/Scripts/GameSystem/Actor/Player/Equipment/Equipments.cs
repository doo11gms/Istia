using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia3.GameSystem.Actor.Player.Equipment
{
    public class Equipments : SerializedMonoBehaviour, Save.ISavable
    {
        void Save.ISavable.Save()
        {
            EquipmentSlots.ForEach(slot => ((Save.ISavable)slot).Save());
        }

        void Save.ISavable.Load()
        {
            EquipmentSlots.ForEach(slot => ((Save.ISavable)slot).Load());
        }

        [OdinSerialize] public List<EquipmentSlot> EquipmentSlots { get; set; } = new List<EquipmentSlot>();

        public EquipmentSlot SearchSlot(int slotID)
        {
            foreach (var slot in EquipmentSlots)
            {
                if (slot.SlotID == slotID) return slot;
            }

            return null;
        }
    }
}