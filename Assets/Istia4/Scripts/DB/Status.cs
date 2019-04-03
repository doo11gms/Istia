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
    [CreateAssetMenu(fileName = "Status", menuName = "Istia4/DB/Status")]
    public class Status : SerializedScriptableObject
    {
        [OdinSerialize] public Dictionary<Parameter, long> ParameterValues { get; set; } = new Dictionary<Parameter, long>();
    }
}