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
    /// <summary>
    /// インベントリのエントリです。
    /// このクラス以外を通じてインベントリを操作した場合、UIとプロファイルに不整合が生じるなど、動作は保証されません。
    /// </summary>
    public class InventoryHandler : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] Profile.InventoryProfile InventoryProfile { get; set; }
        [OdinSerialize, Required] DB.ItemInfoProvider ItemInfoProvider { get; set; }
        [OdinSerialize, Required] Item.ItemCoolDownHandler ItemCoolDownHandler { get; set; }
        [OdinSerialize, Required] UI.Window.InventoryWindow InventoryWindow { get; set; }

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

            InventoryWindow.GetItemSlot((int)tabID, (int)slotID).Assign(itemInfo, itemCount);
            InventoryWindow.GetItemSlot((int)tabID, (int)slotID).Refresh();

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
        public DB.ItemInfo PopItem(int tabID, int slotID)
        {
            var buffer = ItemInfoProvider.Provide(InventoryProfile.GetItemID(tabID, slotID));

            DisposeItem(tabID, slotID);

            return buffer;
        }

        /// <summary>
        /// インベントリ内のアイテムを使用します。
        /// </summary>
        /// <param name="tabID"></param>
        /// <param name="slotID"></param>
        [Button("Use Item")]
        public bool UseItem(int tabID, int slotID)
        {
            var itemInfo = ItemInfoProvider.Provide(InventoryProfile.GetItemID(tabID, slotID));

            if (itemInfo.UsingCoolTime)
            {
                if (!ItemCoolDownHandler.CoolTimeHasFinished(itemInfo))
                {
                    Debug.Log("クールタイム中のため、アイテムを使用できません。");
                    return false;
                }

                ItemCoolDownHandler.Assign(itemInfo);
            }

            if (itemInfo.ItemUsingEffects != null)
            {
                itemInfo.ItemUsingEffects.ForEach(effect => Instantiate(effect));
            }

            if (itemInfo.Consumable) DisposeItem(tabID, slotID);

            return true;
        }

        /// <summary>
        /// インベントリ内の特定のアイテムを1つ破棄します。
        /// </summary>
        /// <param name="tabID"></param>
        /// <param name="slotID"></param>
        [Button("Dispose Item")]
        public bool DisposeItem(int tabID, int slotID)
        {
            if (InventoryProfile.IsEmpty(tabID, slotID)) return false;

            var itemInfo = ItemInfoProvider.Provide(InventoryProfile.GetItemID(tabID, slotID));
            if (!itemInfo.Disposable) return false;

            int itemCount = InventoryProfile.GetItemCount(tabID, slotID) - 1;

            if (itemCount == 0)
            {
                InventoryProfile.Unassign(tabID, slotID);
                InventoryWindow.GetItemSlot(tabID, slotID).Unassign();
            }
            else
            {
                InventoryProfile.Assign(tabID, slotID, itemInfo, itemCount);
                InventoryWindow.GetItemSlot(tabID, slotID).Assign(itemInfo, itemCount);
            }

            InventoryWindow.GetItemSlot(tabID, slotID).Refresh();

            return true;
        }

        /// <summary>
        /// インベントリ内の特定のアイテムをすべて破棄します。
        /// すべてのアイテムが破棄される訳ではありません。
        /// </summary>
        /// <param name="tabID"></param>
        /// <param name="slotID"></param>
        [Button("Dispose Item All")]
        public bool DisposeItemAll(int tabID, int slotID)
        {
            var buf = InventoryProfile.Unassign(tabID, slotID);
            InventoryWindow.GetItemSlot(tabID, slotID).Unassign();
            Refresh();
            return buf;
        }

        /// <summary>
        /// インベントリをソートします。
        /// </summary>
        [Button("Sort")]
        public void Sort()
        {
            Debug.Log("この機能は未実装です。");
        }

        [Button("Refresh")]
        public void Refresh()
        {
            InventoryWindow.Refresh();
        }

        /// <summary>
        /// インベントリを空にします。
        /// </summary>
        [Button("Clean")]
        public void Clean()
        {
            InventoryProfile.Reset();
            InventoryWindow.Initialize();
            InventoryWindow.Refresh();
        }

        private void Awake()
        {
            Refresh();
        }
    }
}