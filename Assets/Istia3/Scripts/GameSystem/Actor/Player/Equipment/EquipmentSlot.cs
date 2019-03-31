using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using EllGames.Istia3.Save;

namespace EllGames.Istia3.GameSystem.Actor.Player.Equipment
{
    public class EquipmentSlot : SerializedMonoBehaviour, ISavable
    {
        void ISavable.Save()
        {
            SaveHandler.Save(this, m_SlotID, nameof(m_SlotID));
            SaveHandler.Save(this, m_CategoryID, nameof(m_CategoryID));
            SaveHandler.Save(this, m_EquipmentInfoID, nameof(m_EquipmentInfoID));
        }

        void ISavable.Load()
        {
            SaveHandler.Load(this, ref m_SlotID, nameof(m_SlotID));
            SaveHandler.Load(this, ref m_CategoryID, nameof(m_CategoryID));
            SaveHandler.Load(this, ref m_EquipmentInfoID, nameof(m_EquipmentInfoID));
        }

        [OdinSerialize] int m_SlotID;
        public int SlotID
        {
            get { return m_SlotID; }
        }

        [OdinSerialize] string m_CategoryID;
        public string CategoryID
        {
            get { return m_CategoryID; }
        }

        [OdinSerialize] string m_EquipmentInfoID;
        public string EquipmentInfoID
        {
            get { return m_EquipmentInfoID; }
        }

        public EquipmentSlot(int slotID = 0, string categoryID = null, string equipmentInfoID = null)
        {
            m_SlotID = slotID;
            m_CategoryID = categoryID;
            m_EquipmentInfoID = equipmentInfoID;
        }

        public bool IsEmpty() => m_EquipmentInfoID == "";

        public void SetContent(string equipmentInfoID)
        {
            m_EquipmentInfoID = equipmentInfoID;
        }

        public void Emptimize()
        {
            m_EquipmentInfoID = null;
        }
    }
}