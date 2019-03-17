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