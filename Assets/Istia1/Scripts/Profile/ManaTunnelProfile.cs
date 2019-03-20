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
            Debug.Log("TODO");
        }

        void Save.ISavable.Load()
        {
        }

        /// <summary>
        /// マナトンネルが開放されていなければtrueを、開放されているならばfalseを指定して下さい。
        /// </summary>
        [OdinSerialize, InfoBox("マナトンネルが閉鎖されているならばtrueを、開放されているならばfalseを指定して下さい。")] Dictionary<DB.ManaTunnelInfo, bool> LockStates { get; set; } = new Dictionary<DB.ManaTunnelInfo, bool>();

        public bool IsLocked(DB.ManaTunnelInfo manaTunnelInfo)
        {
            return LockStates[manaTunnelInfo];
        }

        public void Lock(DB.ManaTunnelInfo manaTunnelInfo)
        {
            LockStates[manaTunnelInfo] = true;
        }

        public void Unlock(DB.ManaTunnelInfo manaTunnelInfo)
        {
            LockStates[manaTunnelInfo] = false;
        }
    }
}