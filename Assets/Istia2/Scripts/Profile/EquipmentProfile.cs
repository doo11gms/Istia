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
        [OdinSerialize] GameObject test;

        void Save.ISavable.Save()
        {
            ES2.Save(test, GetInstanceID().ToString());
            return;
            EquipmentSlots.ForEach(slot =>
            {
                ES2.Save(slot.SlotID, GetInstanceID() + "SlotID");
                ES2.Save(slot.AssignableCategory.Name, GetInstanceID() + "AssignableCategory_Name");

                if (slot.IsEmpty())
                {
                    if (ES2.Exists(GetInstanceID() + "Content_gameObject")) ES2.Delete(GetInstanceID() + "Content_gameObject");
                }
                else
                {
                    ES2.Save(slot.Content.gameObject, GetInstanceID() + "Content_gameObject");
                }
            });
        }

        void Save.ISavable.Load()
        {
            test = ES2.Load<GameObject>(GetInstanceID().ToString());
            return;
            EquipmentSlots.ForEach(slot =>
            {
                slot.SlotID = ES2.Load<int>(GetInstanceID() + "SlotID");
                slot.AssignableCategory.Name = ES2.Load<string>(GetInstanceID() + "AssignableCategory_Name");

                GameObject content = ES2.Load<GameObject>(GetInstanceID() + "Content_gameObject");
                if (content == null)
                {
                    slot.Emptimize();
                }
                else
                {
                    slot.Emptimize();
                    slot.Assign(content.GetComponent<GameSystem.Inventory.Equipment>());
                }
            });
        }

        [OdinSerialize] public List<EquipmentSlot> EquipmentSlots { get; private set; } = new List<EquipmentSlot>();

        public EquipmentSlot SearchSlot(int slotID)
        {
            foreach(var slot in EquipmentSlots)
            {
                if (slot.SlotID == slotID) return slot;
            }

            return null;
        }

        public void Initialize()
        {
            EquipmentSlots = new List<EquipmentSlot>();
        }

        [Title("Buttons")]
        [Button("Emptimize")]
        public void Emptimize()
        {
            EquipmentSlots.ForEach(slot => slot.Emptimize());
        }

        #region EquipmentSlot class

        [System.Serializable]
        public class EquipmentSlot
        {
            [OdinSerialize] int m_SlotID;
            public int SlotID
            {
                get { return m_SlotID; }
                set { m_SlotID = value; }
            }

            [OdinSerialize] DB.Inventory.EquipmentCategory m_AssignableCategory;
            public DB.Inventory.EquipmentCategory AssignableCategory
            {
                get { return m_AssignableCategory; }
                set { m_AssignableCategory = value; }
            }

            [OdinSerialize] GameSystem.Inventory.Equipment m_Content;
            public GameSystem.Inventory.Equipment Content
            {
                get { return m_Content; }
                set { m_Content = value; }
            }

            /// <summary>
            /// スロットが空であるかを判定します。
            /// </summary>
            /// <returns></returns>
            public bool IsEmpty() => m_Content == null;

            /// <summary>
            /// スロットに装備品を登録します。
            /// スロットが既に埋まっている場合はfalseを返します。
            /// </summary>
            /// <param name="equipmentInfo"></param>
            /// <returns></returns>
            public bool Assign(GameSystem.Inventory.Equipment equipment)
            {
                if (equipment.EquipmentInfo.EquipmentCategory == null)
                {
                    Debug.Log("装備品のカテゴリがnullであるため、Assignに失敗しました。");
                    return false;
                }

                if (equipment.EquipmentInfo.EquipmentCategory != m_AssignableCategory) return false;
                if (!IsEmpty()) return false;

                m_Content = equipment;

                return true;
            }

            /// <summary>
            /// 装備品の登録を解除します。
            /// スロットが既に空であり解除対象が存在しない場合、falseを返します。
            /// </summary>
            public bool Unassign()
            {
                if (IsEmpty())
                {
                    Debug.Log("スロットが既に空であり解除対象が存在しないため、Unassignに失敗しました。");
                    return false;
                }

                m_Content = null;

                return true;
            }

            /// <summary>
            /// スロットを空にします。
            /// </summary>
            public void Emptimize()
            {
                m_Content = null;
            }
        }

        #endregion

    }
}