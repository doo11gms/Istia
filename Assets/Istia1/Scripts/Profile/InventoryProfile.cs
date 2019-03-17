﻿using System.Collections;
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
            Dictionary<int, List<int>> shape = new Dictionary<int, List<int>>();

            foreach (var tab in Tabs)
            {
                var slotIDs = new List<int>();

                foreach (var slot in tab.contents)
                {
                    var path = GetInstanceID() + tab.tabID.ToString() + slot.slotID.ToString();

                    if (slot.content == null)
                    {
                        if (ES2.Exists(path + "itemID")) ES2.Delete(path + "itemID");
                        if (ES2.Exists(path + "count")) ES2.Delete(path + "count");
                    }
                    else
                    {
                        ES2.Save(slot.content.itemID, GetInstanceID() + tab.tabID.ToString() + slot.slotID.ToString() + "itemID");
                        ES2.Save(slot.content.count, GetInstanceID() + tab.tabID.ToString() + slot.slotID.ToString() + "count");
                    }

                    slotIDs.Add(slot.slotID);
                }

                shape.Add(tab.tabID, slotIDs);
            }

            ES2.Save(shape, GetInstanceID() + "shape");
        }

        /// <summary>
        /// ロード時のインベントリの形状は、セーブした時と同じ形状である必要があります。
        /// </summary>
        void Save.ISavable.Load()
        {
            foreach (var tab in Tabs)
            {
                foreach (var slot in tab.contents)
                {
                    var path = GetInstanceID() + tab.tabID.ToString() + slot.slotID.ToString();

                    string itemID;
                    if (ES2.Exists(path + "itemID")) itemID = ES2.Load<string>(path + "itemID");
                    else continue;

                    int count;
                    if (ES2.Exists(path + "count")) count = ES2.Load<int>(path + "count");
                    else continue;

                    Assign(tab.tabID, slot.slotID, itemID, count);
                }
            }
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

        /// <summary>
        /// 対象のスロットを探索します。
        /// </summary>
        /// <param name="tabID"></param>
        /// <param name="slotID"></param>
        /// <returns>見つかったスロットを返します。見つからなかった場合、nullを返します。</returns>
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

        /// <summary>
        /// 対象のスロットが存在するか判定します。
        /// </summary>
        /// <param name="tabID"></param>
        /// <param name="slotID"></param>
        /// <returns></returns>
        bool Exists(int tabID, int slotID)
        {
            return SearchSlot(tabID, slotID) != null;
        }

        /// <summary>
        /// プロファイルに情報を登録します。
        /// </summary>
        /// <param name="tabID"></param>
        /// <param name="slotID"></param>
        /// <param name="itemID"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool Assign(int tabID, int slotID, string itemID, int count = 1)
        {
            if (!Exists(tabID, slotID)) return false;

            SearchSlot(tabID, slotID).content = new Content(itemID, count);

            return true;
        }

        /// <summary>
        /// プロファイルに情報を登録します。
        /// </summary>
        /// <param name="tabID"></param>
        /// <param name="slotID"></param>
        /// <param name="itemInfo"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [Button("Assign")]
        public bool Assign(int tabID, int slotID, DB.ItemInfo itemInfo, int count = 1)
        {
            return Assign(tabID, slotID, itemInfo.ItemID, count);
        }

        /// <summary>
        /// プロファイルに登録されている情報を抹消します。
        /// </summary>
        /// <param name="tabID"></param>
        /// <param name="slotID"></param>
        /// <returns></returns>
        [Button("Unassign")]
        public bool Unassign(int tabID, int slotID)
        {
            if (!Exists(tabID, slotID)) return false;

            SearchSlot(tabID, slotID).content = null;

            return true;
        }

        /// <summary>
        /// プロファイルを初期化します。
        /// </summary>
        /// <param name="tabsCount"></param>
        /// <param name="slotsCount"></param>
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

        [Button("Initialize")]
        public void Initialize(Dictionary<int, List<int>> shape)
        {
            Reset();

            Tabs = new List<Tab>();

            foreach (var pair in shape)
            {
                var slots = new List<Slot>();

                for (int i = 0; i < pair.Value.Count; i++)
                {
                    slots.Add(new Slot(pair.Value[i], null));
                }

                Tabs.Add(new Tab(pair.Key, slots));
            }
        }

        /// <summary>
        /// プロファイルをリセットします。
        /// </summary>
        [Button("Reset")]
        public void Reset()
        {
            Initialize(0, 0);
        }
    }
}