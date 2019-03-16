using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia.GameSystem.Item
{
    public class ItemCoolTimeHandler : SerializedMonoBehaviour
    {
        [OdinSerialize, ReadOnly] Dictionary<DB.ItemInfo, float> m_CoolTimeRemains = new Dictionary<DB.ItemInfo, float>();

        public float CoolTimeRemain(DB.ItemInfo itemInfo)
        {
            if (m_CoolTimeRemains.ContainsKey(itemInfo))
            {
                return m_CoolTimeRemains[itemInfo];
            }
            else
            {
                return 0f;
            }
        }

	    public bool CoolTimeHasFinished(DB.ItemInfo itemInfo)
        {
            return !m_CoolTimeRemains.ContainsKey(itemInfo);
        }

        public bool Assign(DB.ItemInfo itemInfo)
        {
            if (m_CoolTimeRemains.ContainsKey(itemInfo)) return false;

            m_CoolTimeRemains.Add(itemInfo, itemInfo.CoolTime);

            return true;
        }

        private void Update()
        {
            var keys = new List<DB.ItemInfo>(m_CoolTimeRemains.Keys);

            keys.ForEach(key =>
            {
                m_CoolTimeRemains[key] -= Time.deltaTime;
                if (m_CoolTimeRemains[key] <= 0f) m_CoolTimeRemains.Remove(key);
            });
        }
    }
}