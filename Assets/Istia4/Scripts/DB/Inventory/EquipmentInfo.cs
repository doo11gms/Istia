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
    [CreateAssetMenu(fileName = "EquipmentInfo", menuName = "Istia4/DB/EquipmentInfo")]
    public class EquipmentInfo : InventoryItemInfoBase
    {
        [Title("Meta")]
        [OdinSerialize] public string ID { get; set; }

        [Title("Category")]
        [OdinSerialize] public EquipmentCategory EquipmentCategory { get; set; }

        [Title("Spec")]
        [OdinSerialize] public Dictionary<Parameter, long> ParameterValues { get; set; } = new Dictionary<Parameter, long>();

        [Title("Constraints")]
        [OdinSerialize] public Dictionary<Parameter, long> Constraints { get; set; } = new Dictionary<Parameter, long>();
    }
}