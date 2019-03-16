using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia.Config
{
    [CreateAssetMenu(menuName = "Profile/SystemProfile")]
    public class SystemProfile : SerializedScriptableObject
    {
        [SerializeField] public bool AutoInventorySort { get; set; } = true;
    }
}