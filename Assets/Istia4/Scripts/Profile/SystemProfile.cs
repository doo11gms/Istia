using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.Profile
{
    [CreateAssetMenu(menuName = "Istia4/Profile/SystemProfile", fileName = "SystemProfile")]
    public class SystemProfile : SerializedScriptableObject
    {
        [SerializeField] public List<string> MapScenePrefixes { get; set; } = new List<string> { "Map_", "SystemMap_", };
    }
}