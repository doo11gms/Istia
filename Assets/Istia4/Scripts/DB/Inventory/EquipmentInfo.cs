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
        [TitleGroup("Basic")]
        [PropertyOrder(13)]
        [OdinSerialize] public EquipmentCategory EquipmentCategory { get; set; }

        [Title("Spec")]
        [PropertyOrder(40)]
        [OdinSerialize] public Dictionary<Parameter, long> ParameterValues { get; set; } = new Dictionary<Parameter, long>();

        [Title("Constraints")]
        [PropertyOrder(41)]
        [OdinSerialize] public Dictionary<Parameter, long> Constraints { get; set; } = new Dictionary<Parameter, long>();
    }
}