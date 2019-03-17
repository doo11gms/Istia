using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia.Provider
{
    [CreateAssetMenu(menuName = "Istia/Provider/ItemInfoProvider")]
    public class ItemInfoProvider : SerializedScriptableObject
    {
        [OdinSerialize] List<DB.ItemInfo> m_ItemInfos = new List<DB.ItemInfo>();

        public DB.ItemInfo Search(string identifier)
        {
            if (m_ItemInfos == null) return null;

            foreach (var info in m_ItemInfos)
            {
                if (info.Identifier == identifier) return info;
            }

            return null;
        }
    }
}