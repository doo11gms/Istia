using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia3.DB.Inventory
{
    [CreateAssetMenu(fileName = "EquipmentCategory", menuName = "Istia3/DB/EquipmentCategory")]
    public class EquipmentCategory : SerializedScriptableObject
    {
        [OdinSerialize] public string ID { get; set; }
        [OdinSerialize] public string Name { get; set; }
    }
}