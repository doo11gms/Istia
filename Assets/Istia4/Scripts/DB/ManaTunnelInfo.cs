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
    [CreateAssetMenu(menuName = "Istia4/DB/ManaTunnelInfo", fileName = "ManaTunnelInfo")]
    public class ManaTunnelInfo : SerializedScriptableObject
    {
        [Title("Settings")]
        [OdinSerialize] public string Name { get; set; }
        [OdinSerialize] public Meta.Location Location { get; set; }
    }
}