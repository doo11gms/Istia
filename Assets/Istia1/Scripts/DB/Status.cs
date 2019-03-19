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
    public class Status : SerializedScriptableObject
    {
        [Title("Basic")]
        [OdinSerialize] public string Name { get; set; } = "NameHere";

        [Title("Spec")]
        [OdinSerialize] public Dictionary<SpecFactorType, long> SpecFactors { get; set; } = new Dictionary<SpecFactorType, long>();
    }
}