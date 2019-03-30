using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia2.DB.Inventory
{
    [CreateAssetMenu(fileName = "EquipmentCategoryProvider", menuName = "Istia2/DB/Privider/EquipmentCategoryProvider")]
    public class EquipmentCategoryProvider : SerializedScriptableObject
    {
        [OdinSerialize] List<EquipmentCategory> Providables { get; set; } = new List<EquipmentCategory>();

        EquipmentCategory Search(string categoryID)
        {
            foreach(var found in Providables)
            {
                if (found.ID == categoryID) return found;
            }

            return null;
        }

        public EquipmentCategory Provide(string categoryID)
        {
            return Search(categoryID);
        }
    }
}