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
        [Title("Basic")]
	    [OdinSerialize] public string StateName { get; set; }
        [OdinSerialize] public float Duration { get; set; } = 60f;

        [Title("Effect")]
        [OdinSerialize] public Status Adder { get; set; }

        [Title("Buttons")]
        public void Initialize()
        {

        }
    }
}