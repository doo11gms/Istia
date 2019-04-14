using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.DB
{
    [CreateAssetMenu(fileName = "Parameter", menuName = "Istia4/DB/Parameter")]
    public class Parameter : SerializedScriptableObject
    {
        [Title("Meta")]
        [OdinSerialize] public string ID { get; set; }

        [Title("Settings")]
        [OdinSerialize] public string Name { get; set; }
        [OdinSerialize] public bool UsingPercentage { get; set; } = false;
    }
}