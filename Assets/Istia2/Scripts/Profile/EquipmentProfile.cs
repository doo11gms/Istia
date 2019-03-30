using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia2.Profile
{
    [CreateAssetMenu(fileName = "EquipmentProfile", menuName = "Istia2/Profile/EquipmentProfile")]
    public class EquipmentProfile : SerializedScriptableObject, Save.ISavable
    {
        void Save.ISavable.Save()
        {
        }

        void Save.ISavable.Load()
        {
        }

        [OdinSerialize] public List<EquipmentSlot> EquipmentSlots { get; set; } = new List<EquipmentSlot>();

        public EquipmentSlot SearchSlot(int slotID)
        {
            foreach(var slot in EquipmentSlots)
            {
                if (slot.SlotID == slotID) return slot;
            }

            return null;
        }

        [Title("Buttons")]
        [Button("Emptimize")]
        public void Emptimize()
        {
            EquipmentSlots.ForEach(slot => slot.Emptimize());
        }

        [Button("New Slot")]
        void NewSlot(DB.Inventory.EquipmentCategory equipmentCategory)
        {
            EquipmentSlots.Add(new EquipmentSlot(EquipmentSlots.Count, equipmentCategory.ID));
            UnityEditor.AssetDatabase.SaveAssets();
        }

        #region EquipmentSlot class

        [System.Serializable]
        public class EquipmentSlot
        {
            [OdinSerialize] int m_SlotID;
            public int SlotID { get; set; }

            [OdinSerialize] string m_AssignableCategoryID;
            public string AssignableCategoryID { get; set; }

            [OdinSerialize] string m_EquipmentInfoID;
            public string EquipmentInfoID { get; set; }

            public EquipmentSlot(int slotID = 0, string assignableCategoryID = null, string equipmentInfoID = null)
            {
                m_SlotID = slotID;
                m_AssignableCategoryID = assignableCategoryID;
                m_EquipmentInfoID = equipmentInfoID;
            }

            public bool IsEmpty() => m_EquipmentInfoID == null;

            public void SetContent(string equipmentInfoID)
            {
                m_EquipmentInfoID = equipmentInfoID;
            }

            public void Emptimize()
            {
                m_EquipmentInfoID = null;
            }
        }

        #endregion

    }
}