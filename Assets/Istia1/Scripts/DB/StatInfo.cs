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
    [CreateAssetMenu(menuName = "Istia1/DB/StatInfo", fileName = "StatInfo")]
    public class StatInfo : SerializedScriptableObject
    {
        [OdinSerialize] public string ID { get; set; }
        [OdinSerialize] public string Name { get; set; }
        [OdinSerialize] public string Abbreviation { get; set; }
        [OdinSerialize] public long DefaultValue { get; set; }
    }
}