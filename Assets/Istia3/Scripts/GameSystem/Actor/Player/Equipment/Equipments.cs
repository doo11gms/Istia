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
        [Button("TestSave")]
        void TestSave()
        {
            foreach (var slot in EquipmentSlots)
            {
                Debug.Log(slot.SlotID);
                Debug.Log(slot.CategoryID);
                string path = GetInstanceID().ToString() + slot.SlotID;
                ES2.Save(slot.SlotID, path);
                ES2.Save(slot.CategoryID, path + slot.CategoryID.ToString());
                if (slot.EquipmentInfoID != null) ES2.Save(slot.EquipmentInfoID, path + slot.EquipmentInfoID.ToString());
            }
        }

        [Button("Save")]
        void Save.ISavable.Save()
        {
            EquipmentSlots.ForEach(slot =>
            {
                string path = GetInstanceID().ToString() + slot.SlotID;
                ES2.Save(slot.SlotID, path);
                ES2.Save(slot.CategoryID, path + slot.CategoryID.ToString());
                ES2.Save(slot.EquipmentInfoID, path + slot.EquipmentInfoID.ToString());
            });
        }

        [Button("Load")]
        void Save.ISavable.Load()
        {

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
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
        }

        #region EquipmentSlot class

        [System.Serializable]
        public class EquipmentSlot
        {
            [OdinSerialize] int m_SlotID;
            public int SlotID { get; set; }

            [OdinSerialize] string m_CategoryID;
            public string CategoryID { get; set; }

            [OdinSerialize] string m_EquipmentInfoID;
            public string EquipmentInfoID { get; set; }

            public EquipmentSlot(int slotID = 0, string categoryID = null, string equipmentInfoID = null)
            {
                m_SlotID = slotID;
                m_CategoryID = categoryID;
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