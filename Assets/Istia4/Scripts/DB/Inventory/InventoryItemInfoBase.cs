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
        [Title("Graphic")]
        [PropertyOrder(10)]
        [OdinSerialize, PreviewField] public Sprite IconSprite { get; private set; }

        [Title("Meta")]
        [PropertyOrder(11)]
        [OdinSerialize] public string ID { get; set; }

        [TitleGroup("Basic")]
        [PropertyOrder(12)]
        [OdinSerialize] public string Name { get; set; }

        [Title("Droppable")]
        [PropertyOrder(30)]
        [OdinSerialize] public bool Droppable { get; private set; } = true;

        [PropertyOrder(31)]
        [OdinSerialize, EnableIf("Disposable"), PreviewField] public GameObject Model { get; private set; }
    }
}