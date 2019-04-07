using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.GameSystem.Prop
{
    /// <summary>
    /// アタッチしたオブジェクトをアイテムルーティングによって拾得可能なオブジェクトにします。
    /// </summary>
    public class Lootable : SerializedMonoBehaviour
    {
        [OdinSerialize] public DB.Inventory.InventoryItemInfoBase InventoryItemInfo { get; set; }
        [OdinSerialize] public int Count { get; set; } = 1;

        [OdinSerialize] string NamePlateTextForDebug { get; set; }

        public Lootable(DB.Inventory.InventoryItemInfoBase inventoryItemInfo, int count = 1)
        {
            InventoryItemInfo = inventoryItemInfo;
            Count = count;
        }

        void NamePlateUpdate()
        {
            if (Count > 1)
            {
                NamePlateTextForDebug = InventoryItemInfo.Name + "(" + Count.ToString() + ")";
            }
            else
            {
                NamePlateTextForDebug = InventoryItemInfo.Name;
            }
        }

        public DB.Inventory.InventoryItemInfoBase Loot()
        {
            Count--;
            return InventoryItemInfo;
        }

        private void Update()
        {
            NamePlateUpdate();

            if (Count < 0) throw new System.Exception("不正な値が指定されています。");
            if (Count == 0) Destroy(gameObject);
        }
    }
}