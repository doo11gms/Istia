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
    [CreateAssetMenu(menuName = "Istia4/Handler/DropHandler", fileName = "DropHandler")]
    public class DropHandler : SerializedScriptableObject
    {
        [OdinSerialize] GameObject DefaultModel { get; set; }

	    public void Drop(Vector3 position, DB.Inventory.InventoryItemInfoBase inventoryItemInfo, int count = 1)
        {
            var model = inventoryItemInfo.Model;
            if (model == null) model = DefaultModel;

            var dropped = Instantiate(model, position, model.transform.rotation);
            dropped.AddComponent<Lootable>();
            dropped.GetComponent<Lootable>().InventoryItemInfo = inventoryItemInfo;
            dropped.GetComponent<Lootable>().Count = count;
        }
    }
}