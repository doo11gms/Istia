using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.DB
{
    [CreateAssetMenu(menuName = "Istia1/Provider/ItemInfoProvider", fileName = "ItemInfoProvider")]
    public class ItemInfoProvider : SerializedScriptableObject
    {
        [OdinSerialize] List<DB.ItemInfo> ItemInfos { get; set; } = new List<DB.ItemInfo>();

        public DB.ItemInfo Provide(string identifier)
        {
            if (ItemInfos == null) return null;

            foreach (var info in ItemInfos)
            {
                if (info.Identifier == identifier) return info;
            }

            return null;
        }
    }
}