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
    [CreateAssetMenu(menuName = "Istia1/Profile/PlayerProfile", fileName = "PlayerProfile")]
    public class PlayerProfile : SerializedScriptableObject, Save.ISavable
    {
        void Save.ISavable.Save()
        {
            if (StatInfos == null) throw new System.Exception("StatInfosがnullであるため、セーブできません。");
            ES2.Save(Name, GetInstanceID() + "Name");
            ES2.Save(StatInfos, GetInstanceID() + "StatInfos");
        }

        void Save.ISavable.Load()
        {
            if (StatInfos == null) throw new System.Exception("StatInfosがnullであるため、ロードできません。");
            Name = ES2.Load<string>(GetInstanceID() + "Name");
            StatInfos = ES2.LoadDictionary<DB.StatInfo, long>(GetInstanceID() + "StatInfos");
        }

        [OdinSerialize] public string Name { get; set; }
        [OdinSerialize] public Dictionary<DB.StatInfo, long> StatInfos { get; set; } = new Dictionary<DB.StatInfo, long>();

        [Button("Reset")]
        public void Reset()
        {
            (new List<DB.StatInfo>(StatInfos.Keys)).ForEach(key => StatInfos[key] = key.DefaultValue);
        }
    }
}