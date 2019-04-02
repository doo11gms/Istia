using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.DB.Inventory
{
    [CreateAssetMenu(fileName = "ItemCategory", menuName = "Istia4/DB/ItemCategory")]
    public class ItemCategory : SerializedScriptableObject
    {
        [OdinSerialize] public string ID { get; set; }
        [OdinSerialize] public string Name { get; set; }
    }
}