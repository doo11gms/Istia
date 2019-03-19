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
    [CreateAssetMenu(menuName = "Istia1/DB/Status", fileName = "Status")]
    [System.Serializable]
    public class Status : SerializedScriptableObject
    {
        [Title("Basic")]
        [OdinSerialize] public string Name { get; set; } = "NameHere";

        [Title("Stats")]
        [OdinSerialize] public Dictionary<StatInfo, long> Stats { get; set; } = new Dictionary<StatInfo, long>();

        [Button("Reset")]
        public void Reset()
        {
            (new List<StatInfo>(Stats.Keys)).ForEach(key => Stats[key] = key.DefaultValue);
        }
    }
}