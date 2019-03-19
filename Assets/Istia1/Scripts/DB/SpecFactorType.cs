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
    [CreateAssetMenu(menuName = "Istia1/DB/SpecFactorType", fileName = "SpecFactorType")]
    public class SpecFactorType : SerializedScriptableObject
    {
        [Title("Settings")]
        [OdinSerialize] public string ID { get; set; }
        [OdinSerialize] public string Name { get; set; }
        [OdinSerialize] public string Abbreviation { get; set; }
        [OdinSerialize] public bool Percentage { get; set; } = false;
    }
}