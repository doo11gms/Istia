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
    [CreateAssetMenu(menuName = "Istia1/DB/Avator", fileName = "Avator")]
    public class Avator : SerializedScriptableObject
    {
	    [OdinSerialize] public string Name { get; set; }
        [OdinSerialize] public GameObject Model { get; set; }
    }
}