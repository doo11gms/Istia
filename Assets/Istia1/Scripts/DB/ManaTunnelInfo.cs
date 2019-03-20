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
    [CreateAssetMenu(menuName = "Istia1/DB/ManaTunnelInfo", fileName = "ManaTunnelInfo")]
    public class ManaTunnelInfo : SerializedScriptableObject
    {
        [Title("Settings")]
        [OdinSerialize] public string Name { get; set; }
        [OdinSerialize] public string SceneName { get; set; }
        [OdinSerialize] public Vector3 Point { get; set; }
        [OdinSerialize] public Vector3 EulerAngles { get; set; }
    }
}