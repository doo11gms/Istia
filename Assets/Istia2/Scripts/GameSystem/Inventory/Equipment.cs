using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia2.GameSystem.Inventory
{
    public class Equipment : InventoryItemBase
    {
	    [OdinSerialize] public DB.Inventory.EquipmentInfo EquipmentInfo { get; set; }
        [OdinSerialize] public DB.Inventory.EnchantInfo EnchantInfo { get; set; }
    }
}