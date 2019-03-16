using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia.Profile
{
    [CreateAssetMenu(menuName = "Profile/InventoryProfile")]
    public class InventoryProfile : SerializedScriptableObject
    {
	    [OdinSerialize] public List<List<string>> Contents { get; set; }
    }
}