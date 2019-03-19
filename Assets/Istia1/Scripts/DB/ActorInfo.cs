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
    [CreateAssetMenu(menuName = "Istia1/DB/ActorInfo", fileName = "ActorInfo")]
    public class ActorInfo : SerializedScriptableObject
    {
        [OdinSerialize] public string ActorName { get; set; } = "ActorNameHere";
    }
}