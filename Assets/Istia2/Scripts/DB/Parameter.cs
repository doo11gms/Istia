using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia2.DB
{
    [CreateAssetMenu(fileName = "Parameter", menuName = "Istia2/DB/Parameter")]
    public class Parameter : SerializedScriptableObject
    {
        [Title("Basic")]
        [OdinSerialize] public string Name { get; set; }
        [OdinSerialize] public string Abbreviation { get; set; }

        [Title("Advanced")]
        [OdinSerialize] public bool UsingPercentage { get; set; } = false;
    }
}