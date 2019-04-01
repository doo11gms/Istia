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
    public abstract class InventoryItemInfoBase : SerializedScriptableObject
    {
        [Title("Drop")]
        [OdinSerialize] public bool Droppable { get; private set; } = true;
        [OdinSerialize, EnableIf("Droppable")] public GameObject Model { get; private set; }

        [Title("Graphic")]
        [OdinSerialize] public Sprite IconSprite { get; private set; }
    }
}