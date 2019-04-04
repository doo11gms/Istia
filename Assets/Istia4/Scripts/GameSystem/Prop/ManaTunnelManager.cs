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
    public class ManaTunnelManager : SerializedMonoBehaviour, Save.ISavable
    {
        void Save.ISavable.Save()
        {
            if (m_ActiveStates == null) return;
            foreach (var key in m_ActiveStates.Keys)
            {
                Debug.Log(key.name);
                Save.SaveHandler.Save(this, m_ActiveStates[key], key.name);
            }
        }

        void Save.ISavable.Load()
        {
            if (m_ActiveStates == null) return;
            var keys = new List<DB.ManaTunnelInfo>(m_ActiveStates.Keys);
            foreach(var key in keys)
            {
                m_ActiveStates[key] = Save.SaveHandler.Load<bool>(this, key.name);
            }
        }

        [OdinSerialize] Dictionary<DB.ManaTunnelInfo, bool> m_ActiveStates = new Dictionary<DB.ManaTunnelInfo, bool>();

        /// <summary>
        /// マナトンネルが活性化されている場合はtrueを、活性化されていない場合はfalseを返します。
        /// </summary>
        /// <param name="manaTunnelInfo"></param>
        /// <returns></returns>
        public bool IsActive(DB.ManaTunnelInfo manaTunnelInfo)
        {
            return m_ActiveStates[manaTunnelInfo];
        }

        /// <summary>
        /// マナトンネルを活性化します。
        /// </summary>
        public void Activate(DB.ManaTunnelInfo manaTunnelInfo)
        {
            m_ActiveStates[manaTunnelInfo] = true;
        }

        /// <summary>
        /// マナトンネルを非活性化します。
        /// </summary>
        public void Deactivate(DB.ManaTunnelInfo manaTunnelInfo)
        {
            m_ActiveStates[manaTunnelInfo] = false;
        }
    }
}