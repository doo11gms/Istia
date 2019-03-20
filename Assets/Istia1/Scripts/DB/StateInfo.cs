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
    [CreateAssetMenu(menuName = "Istia1/DB/StateInfo", fileName = "StateInfo")]
    public class StateInfo : SerializedScriptableObject
    {
        [Title("Graphic")]
        [OdinSerialize, PreviewField] public Sprite IconSprite { get; set; }

        [Title("Basic")]
	    [OdinSerialize] public string StateName { get; set; }
        [OdinSerialize] public float Duration { get; set; } = 60f;

        [Title("Status Correciton")]
        [OdinSerialize] Dictionary<SpecFactorType, long> AddingSpecValues { get; set; } = new Dictionary<SpecFactorType, long>();
    }
}