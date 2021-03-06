﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.DB.Inventory
{
    [CreateAssetMenu(fileName = "ItemInfoProvider", menuName = "Istia4/Provider/ItemInfoProvider")]
    public class ItemInfoProvider : SerializedScriptableObject
    {
        [OdinSerialize] List<ItemInfo> Providables { get; set; } = new List<ItemInfo>();

        ItemInfo Search(string itemInfoID)
        {
            foreach (var found in Providables)
            {
                if (found.ID == itemInfoID) return found;
            }

            return null;
        }

        public ItemInfo Provide(string itemInfoID)
        {
            var found = Search(itemInfoID);
            if (found == null) throw new System.Exception("ItemInfoIDに対応するItemInfoが見つかりません。");
            return found;
        }
    }
}