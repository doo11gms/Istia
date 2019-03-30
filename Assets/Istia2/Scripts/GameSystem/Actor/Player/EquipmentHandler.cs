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
        public bool Equip(DB.Inventory.EquipmentInfo equipmentInfo)
        {
            foreach(var slot in EquipmentProfile.EquipmentSlots)
            {
                Debug.Log(slot.AssignableCategoryID);
                Debug.Log(equipmentInfo.EquipmentCategory.ID);
                Debug.Log(slot.IsEmpty());
                Debug.Log("");
                if (slot.AssignableCategoryID != equipmentInfo.EquipmentCategory.ID) continue;
                if (slot.IsEmpty())
                {
                    slot.SetContent(equipmentInfo.ID);
                    return true;
                }
                else
                {
                    continue;
                }
            }

            return false;
        }

        [Button("Unequip")]
        public bool Unequip(int slotID)
        {
            var slot = EquipmentProfile.SearchSlot(slotID);

            if (slot == null)
            {
                Debug.LogError("対象のスロットが見つからなかったため、装備解除に失敗しました。");
                return false;
            }

            if (slot.IsEmpty())
            {
                Debug.LogError("既に空であるスロットに対して装備解除できません。");
                return false;
            }

            slot.Emptimize();

            return true;
        }

        [Button("Unequip All")]
        public void UnequipAll()
        {
            EquipmentProfile.EquipmentSlots.ForEach(slot => slot.Emptimize());
        }

        /// <summary>
        /// インベントリを最新の状態に更新します。
        /// </summary>
        [Button("Refresh")]
        public void Refresh()
        {
            Debug.Log("TODO");
        }
    }
}