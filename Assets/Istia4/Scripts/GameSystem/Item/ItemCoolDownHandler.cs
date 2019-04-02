using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.GameSystem.Item
{
    public class ItemCoolDownHandler : SerializedMonoBehaviour
    {
        [OdinSerialize] Dictionary<DB.Inventory.ItemInfo, float> m_CoolTimeRemains = new Dictionary<DB.Inventory.ItemInfo, float>();

        /// <summary>
        /// 残りのクールタイムを返します。
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public float CoolTimeRemain(DB.Inventory.ItemInfo itemInfo)
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

        /// <summary>
        /// クールタイムが終了しているか否かを返します。
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public bool CoolTimeFinished(DB.Inventory.ItemInfo itemInfo)
        {
            return !m_CoolTimeRemains.ContainsKey(itemInfo);
        }

        /// <summary>
        /// 対象をクールタイム監視下に置きます。
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public bool Assign(DB.Inventory.ItemInfo itemInfo)
        {
            if (m_CoolTimeRemains.ContainsKey(itemInfo)) return false;

            m_CoolTimeRemains.Add(itemInfo, itemInfo.CoolTime);

            return true;
        }

        private void Update()
        {
            var keys = new List<DB.Inventory.ItemInfo>(m_CoolTimeRemains.Keys);

            keys.ForEach(key =>
            {
                m_CoolTimeRemains[key] -= Time.deltaTime;
                if (m_CoolTimeRemains[key] <= 0f) m_CoolTimeRemains.Remove(key);
            });
        }
    }
}