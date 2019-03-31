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
    public class EquipmentHandler : SerializedMonoBehaviour
    {
        [OdinSerialize, Required] Equipments Equipments { get; set; }
        [OdinSerialize, Required] UI.Window.EquipmentWindow EquipmentWindow { get; set; }
        [OdinSerialize, Required] DB.Inventory.EquipmentInfoProvider EquipmentInfoProvider { get; set; }

        [Title("Buttons")]
        [Button("Equip")]
        public bool Equip(DB.Inventory.EquipmentInfo equipmentInfo)
        {
            foreach(var slot in Equipments.EquipmentSlots)
            {
                if (slot.CategoryID != equipmentInfo.EquipmentCategory.ID) continue;
                if (!slot.IsEmpty()) continue;

                var UISlot = EquipmentWindow.SearchSlot(slot.SlotID);
                if (UISlot == null) throw new System.Exception("UISlotが見つかりません。");

                var info = EquipmentInfoProvider.Provide(equipmentInfo.ID);
                if (info == null) throw new System.Exception("EquipmentInfoがnullです。");

                slot.SetContent(equipmentInfo.ID);
                UISlot.Assign(info);
                return true;
            }

            return false;
        }

        [Button("Unequip")]
        public void Unequip(int slotID)
        {
            var slot = Equipments.SearchSlot(slotID);
            if (slot == null) throw new System.Exception("対象のスロットが存在しません。");

            var slotUI = EquipmentWindow.SearchSlot(slot.SlotID);
            if (slotUI == null) throw new System.Exception("対象のUIスロットがnullです。");

            slot.Emptimize();
            slotUI.Unassign();
        }

        [Button("Unequip All")]
        public void UnequipAll()
        {
            Equipments.EquipmentSlots.ForEach(slot => slot.Emptimize());
            EquipmentWindow.EquipmentSlots.ForEach(slot => slot.Unassign());
        }

        [Button("Save")]
        public void Save()
        {
            Equipments.EquipmentSlots.ForEach(slot => ((EllGames.Istia3.Save.ISavable)slot).Save());
        }

        [Button("Load")]
        public void Load()
        {
            Equipments.EquipmentSlots.ForEach(slot => ((EllGames.Istia3.Save.ISavable)slot).Load());
        }

        [Button("Refresh")]
        public void Refresh()
        {
            foreach(var slot in Equipments.EquipmentSlots)
            {
                var uiSlot = EquipmentWindow.SearchSlot(slot.SlotID);
                if (uiSlot == null) throw new System.Exception("対象のUIスロットが見つかりません。");

                if (slot.IsEmpty())
                {
                    uiSlot.Unassign();
                    continue;
                }
                else
                {
                    var info = EquipmentInfoProvider.Provide(slot.EquipmentInfoID);
                    if (info == null) throw new System.Exception("対象のEquipmentInfoが見つかりません。");

                    uiSlot.Assign(info);
                }
            }
        }
    }
}