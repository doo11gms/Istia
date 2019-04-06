using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.GameSystem.Prop
{
    public class DropItem : SerializedMonoBehaviour
    {
        [OdinSerialize] public DB.Inventory.InventoryItemInfoBase InventoryItemInfo { get; set; }


    }
}