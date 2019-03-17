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

        int? TabID(DB.ItemInfo itemInfo)
        {
            if (itemInfo.Consumable)
            {
                return ConsumableTabID;
            }

            return null;
        }
        /*
        int? PushableSlotID(DB.ItemInfo itemInfo)
        {
            var tabID = TabID(itemInfo);

            if (tabID == null)
            {
                return null;
            }
            else
            {
                var candidates = InventoryProfile.GetChildSlotIDs((int)tabID);

                if (candidates == null) return null;

                foreach (var slotID in candidates)
                {
                    if (Equals(itemInfo, ItemInfoProvider.Provide(InventoryProfile.GetIdentifier((int)tabID, slotID))))
                    {
                        if (InventoryProfile.GetCount((int)tabID, slotID) + 1 <= itemInfo.MaxStackCount) return slotID;
                    }
                }
            }

            return null;
        }

        [Title("Buttons")]

        /// <summary>
        /// インベントリを初期化します。
        /// </summary>
        [Button("Initialize")]
        public void InventoryInitialize()
        {
            InventoryProfile.Initialize();
        }
        
        /// <summary>
        /// インベントリにアイテムを追加します。
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        [Button("Push Item")]
        public bool PushItem(DB.ItemInfo itemInfo)
        {
            var slotID = PushableSlotID(itemInfo);

            if (slotID == null) return false;

            InventoryProfile.Assign((int)TabID(itemInfo), (int)slotID, itemInfo.Identifier);

            return true;
        }
        */
        /// <summary>
        /// インベントリにアイテムを追加します。
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool PushItem(DB.ItemInfo itemInfo, int count = 1)
        {

            return false;
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
        /// UIを更新します。
        /// </summary>
        [Button("UI Update")]
        public void UIUpdate()
        {
            
        }
    }
}