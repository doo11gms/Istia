using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.GameSystem.Actor
{
    public class InventoryHandler : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] Profile.InventoryProfile InventoryProfile { get; set; }
        [OdinSerialize, Required] DB.ItemInfoProvider ItemInfoProvider { get; set; }

        [Title("Settings")]
        [OdinSerialize] int ConsumableTabID { get; set; } = 0;
        [OdinSerialize] int EquipmentTabID { get; set; } = 1;
        [OdinSerialize] int OtherTabID { get; set; } = 2;
        [OdinSerialize] int MaterialTabID { get; set; } = 3;
        [OdinSerialize] int QuestTabID { get; set; } = 4;

        /// <summary>
        /// ItemInfoから適当なタブIDを取得します。
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        int? GetTabID(DB.ItemInfo itemInfo)
        {
            if (itemInfo.Consumable) return ConsumableTabID;

            return null;
        }

        /// <summary>
        /// アイテムをプッシュ可能なスロットIDを返します。
        /// 空のスロットよりもスタック可能なスロットが優先されます。
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        int? PushableSlotID(DB.ItemInfo itemInfo)
        {
            var tabID = GetTabID(itemInfo);

            if (tabID == null) return null;

            int? emptySlotID = null;

            foreach(var slotID in InventoryProfile.ChildSlotIDs((int)tabID))
            {
                if (InventoryProfile.IsEmpty((int)tabID, slotID))
                {
                    if (emptySlotID == null) emptySlotID = slotID;
                    continue;
                }

                if (itemInfo.ItemID == InventoryProfile.GetItemID((int)tabID, slotID))
                {
                    if (InventoryProfile.GetItemCount((int)tabID, slotID) + 1 <= itemInfo.MaxStackCount) return slotID;
                }
            }

            return emptySlotID;
        }

        /// <summary>
        /// インベントリにアイテムを追加します。
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public bool PushItem(DB.ItemInfo itemInfo)
        {
            if (itemInfo == null) throw new System.Exception("nullなアイテムをプッシュすることはできません。");

            var slotID = PushableSlotID(itemInfo);
            if (slotID == null) return false;

            var tabID = GetTabID(itemInfo);
            if (tabID == null) return false;

            var itemCount = 1;
            if (!InventoryProfile.IsEmpty((int)tabID, (int)slotID)) itemCount = InventoryProfile.GetItemCount((int)tabID, (int)slotID) + 1;

            return InventoryProfile.Assign((int)tabID, (int)slotID, itemInfo, itemCount);
        }

        /// <summary>
        /// インベントリにアイテムを追加します。
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [Button("Push Item")]
        public bool PushItem(DB.ItemInfo itemInfo, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                if (!PushItem(itemInfo)) return false;
            }

            return true;
        }

        /// <summary>
        /// インベントリからアイテムを取り出します。
        /// </summary>
        /// <param name="tabID"></param>
        /// <param name="slotID"></param>
        /// <returns></returns>
        [Button("Pop Item")]
        public DB.ItemInfo PopItem(int tabID, int slotID)
        {
            return null;
        }

        /// <summary>
        /// インベントリ内のアイテムを使用します。
        /// </summary>
        /// <param name="tabID"></param>
        /// <param name="slotID"></param>
        [Button("Use Item")]
        public void UseItem(int tabID, int slotID)
        {

        }

        /// <summary>
        /// インベントリ内の特定のアイテムを1つ破棄します。
        /// </summary>
        /// <param name="tabID"></param>
        /// <param name="slotID"></param>
        [Button("Dispose")]
        public void Dispose(int tabID, int slotID)
        {

        }

        /// <summary>
        /// インベントリ内の特定のアイテムをすべて破棄します。
        /// すべてのアイテムが破棄される訳ではありません。
        /// </summary>
        /// <param name="tabID"></param>
        /// <param name="slotID"></param>
        [Button("Dispose All")]
        public void DisposeAll(int tabID, int slotID)
        {

        }

        /// <summary>
        /// インベントリをソートします。
        /// </summary>
        [Button("Sort")]
        public void Sort()
        {
            Debug.Log("この機能は未実装です。");
        }

        /// <summary>
        /// インベントリの内容を最新の状態に更新します。
        /// </summary>
        [Button("Refresh")]
        public void Refresh()
        {
        }
    }
}