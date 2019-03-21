using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.Profile
{
    [CreateAssetMenu(menuName = "Istia1/Profile/ManaTunnelProfile", fileName = "ManaTunnelProfile")]
    public class ManaTunnelProfile : SerializedScriptableObject, Save.ISavable
    {
        void Save.ISavable.Save()
        {
            ES2.Save(ActiveStates, GetInstanceID() + "ActiveStates");
        }

        void Save.ISavable.Load()
        {
            ActiveStates = ES2.LoadDictionary<DB.ManaTunnelInfo, bool>(GetInstanceID() + "ActiveStates");
        }

        [OdinSerialize] Dictionary<DB.ManaTunnelInfo, bool> ActiveStates { get; set; } = new Dictionary<DB.ManaTunnelInfo, bool>();

        /// <summary>
        /// マナトンネルが活性化されている場合はtrueを、活性化されていない場合はfalseを返します。
        /// </summary>
        /// <param name="manaTunnelInfo"></param>
        /// <returns></returns>
        public bool IsActive(DB.ManaTunnelInfo manaTunnelInfo)
        {
            return ActiveStates[manaTunnelInfo];
        }

        /// <summary>
        /// マナトンネルを活性化します。
        /// </summary>
        public void Activate(DB.ManaTunnelInfo manaTunnelInfo)
        {
            ActiveStates[manaTunnelInfo] = true;
        }

        /// <summary>
        /// マナトンネルを非活性化します。
        /// </summary>
        public void Deactivate(DB.ManaTunnelInfo manaTunnelInfo)
        {
            ActiveStates[manaTunnelInfo] = false;
        }
    }
}