using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Linq;

namespace EllGames.Istia1.Profile
{
    [CreateAssetMenu(menuName = "Istia1/Profile/InventoryProfile", fileName = "InventoryProfile")]
    public class InventoryProfile : SerializedScriptableObject, Save.ISavable
    {
        /// <summary>
        /// インベントリの中身が保存されるだけで、インベントリの形状は保存されません。
        /// </summary>
        void Save.ISavable.Save()
        {
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
            }
        }

        /// <summary>
        /// インベントリの中身が復元されるだけで、インベントリの形状は復元されません。
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
        /// 対象のタブを検索します。
        /// </summary>
        /// <param name="tabID"></param>
        /// <returns></returns>
        Tab SearchTab(int tabID)
        {
            if (Tabs == null) return null;

            foreach (var tab in Tabs)
            {
                if (tab.tabID == tabID) return tab;
            }

            return null;
        }

        bool Exists(int tabID)
        {
            return SearchTab(tabID) != null;
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
        /// 新しくスロットを追加します。
        /// </summary>
        /// <param name="tabID"></param>
        /// <param name="slotID"></param>
        /// <returns>スロットの追加に成功した場合true、失敗した場合falseを返します。</returns>
        public bool AddSlot(int tabID, int slotID)
        {
            if (Exists(tabID, slotID)) return false;

            SearchTab(tabID).contents.Add(new Slot(slotID));

            return true;
        }

        public string GetItemID(int tabID, int slotID)
        {
            return SearchSlot(tabID, slotID).content.itemID;
        }

        public int GetItemCount(int tabID, int slotID)
        {
            return SearchSlot(tabID, slotID).content.count;
        }

        public bool IsEmpty(int tabID, int slotID)
        {
            if (!Exists(tabID, slotID)) return false;

            return SearchSlot(tabID, slotID).content == null;
        }

        public List<int> GetAllTabIDs()
        {
            return Tabs.Select(tab => tab.tabID).ToList();
        }

        public List<int> GetAllSlotIDs(int tabID)
        {
            return SearchTab(tabID).contents.Select(content => content.slotID).ToList();
        }

        /// <summary>
        /// 対象のタブに属するスロットを返します。
        /// </summary>
        /// <param name="tabID"></param>
        /// <returns></returns>
        public List<int> ChildSlotIDs(int tabID)
        {
            if (!Exists(tabID)) return null;

            return SearchTab(tabID).contents.Select(content => content.slotID).ToList();
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

        /// <summary>
        /// プロファイルをリセットします。
        /// </summary>
        [Button("Reset")]
        public void Reset()
        {
            foreach(var tab in Tabs)
            {
                foreach(var slot in tab.contents)
                {
                    slot.content = null;
                }
            }
        }
    }
}