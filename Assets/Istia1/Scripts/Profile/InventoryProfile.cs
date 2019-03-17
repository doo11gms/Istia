using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.Profile
{
    [CreateAssetMenu(menuName = "Istia1/Profile/InventoryProfile", fileName = "InventoryProfile")]
    public class InventoryProfile : SerializedScriptableObject, Save.ISavable
    {
        void Save.ISavable.Save()
        {
            // TODO
        }

        void Save.ISavable.Load()
        {
            // TODO
        }

        [System.Serializable]
        class Content
        {
            public string itemID;
            public int count;

            public Content(string itemID = null, int count = 0)
            {
                this.itemID = itemID;
                this.count = count;
            }
        }

        [System.Serializable]
        class Slot
        {
            public int slotID;
            public Content content;

            public Slot(int slotID = -1, Content content = null)
            {
                this.slotID = slotID;
                this.content = content;
            }
        }

        [System.Serializable]
        class Tab
        {
            public int tabID;
            public List<Slot> contents;

            public Tab(int tabID = -1, List<Slot> contents = null)
            {
                this.tabID = tabID;
                this.contents = contents;
            }
        }

        [OdinSerialize] List<Tab> Tabs { get; set; } = new List<Tab>();

        Slot SearchSlot(int tabID, int slotID)
        {
            if (Tabs == null) return null;

            foreach (var tab in Tabs)
            {
                if (tab.tabID == tabID)
                {
                    foreach(var slot in tab.contents)
                    {
                        if (slot.slotID == slotID) return slot;
                    }
                }
            }

            return null;
        }

        bool Exists(int tabID, int slotID)
        {
            return SearchSlot(tabID, slotID) != null;
        }

        public bool Assign(int tabID, int slotID, string itemID, int count = 1)
        {
            if (!Exists(tabID, slotID)) return false;

            SearchSlot(tabID, slotID).content = new Content(itemID, count);

            return true;
        }

        [Button("Assign")]
        public bool Assign(int tabID, int slotID, DB.ItemInfo itemInfo, int count = 1)
        {
            return Assign(tabID, slotID, itemInfo.Identifier, count);
        }

        [Button("Unassign")]
        public bool Unassign(int tabID, int slotID)
        {
            if (!Exists(tabID, slotID)) return false;

            SearchSlot(tabID, slotID).content = null;

            return true;
        }

        [Button("Initialize")]
        public void Initialize(int tabsCount = 0, int slotsCount = 0)
        {
            if (!(0 <= tabsCount && tabsCount <= 10)) throw new System.Exception("このメソッドにより生成するタブの数は0から10の範囲で指定して下さい。");
            if (!(0 <= slotsCount && slotsCount <= 100)) throw new System.Exception("このメソッドにより生成するスロットの数は0から100の範囲で指定して下さい。");
            if (tabsCount == 0 && slotsCount > 0) throw new System.Exception("生成できないインベントリの形状です。");
            if (tabsCount < 0 || slotsCount < 0) throw new System.Exception("生成できないインベントリの形状です。");
            
            Tabs = new List<Tab>();

            for (int i = 0; i < tabsCount; i++)
            {
                var slots = new List<Slot>();

                for (int j = 0; j < slotsCount; j++)
                {
                    slots.Add(new Slot(j, null));
                }

                Tabs.Add(new Tab(i, slots));
            }
        }

        [Button("Reset")]
        public void Reset()
        {
            Initialize(0, 0);
        }
    }
}