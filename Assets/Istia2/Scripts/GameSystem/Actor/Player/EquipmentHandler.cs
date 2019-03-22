using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia2.GameSystem.Actor.Player
{
    /// <summary>
    /// 装備システムを安全に扱う機能を提供するエントリです。
    /// </summary>
    public class EquipmentHandler : SerializedMonoBehaviour
    {
        [OdinSerialize, Required] Profile.EquipmentProfile EquipmentProfile { get; set; }

        [Title("Buttons")]
        [Button("Equip")]
        public bool Equip(Inventory.Equipment equipment)
        {
            foreach(var slot in EquipmentProfile.EquipmentSlots)
            {
                if (slot.Assign(equipment)) return true;
            }

            return false;
        }

        [Button("Unequip")]
        public bool Unequip(int slotID)
        {
            var found = EquipmentProfile.SearchSlot(slotID);

            if (found == null)
            {
                Debug.Log("対象のスロットが見つからなかったため、装備解除に失敗しました。");
                return false;
            }

            return found.Unassign();
        }

        [Button("Unequip All")]
        public void UnequipAll()
        {
            foreach(var slot in EquipmentProfile.EquipmentSlots)
            {
                if (!slot.IsEmpty()) slot.Unassign();
            }
        }
    }
}