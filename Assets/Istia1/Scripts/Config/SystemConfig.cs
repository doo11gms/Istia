using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.Config
{
    [CreateAssetMenu(menuName = "Istia1/Config/SystemConfig", fileName = "SystemConfig")]
    public class SystemConfig : SerializedScriptableObject
    {
        [SerializeField] public List<string> MapScenePrefixes { get; set; } = new List<string> { "Map_", "SystemMap_", };
    }
}