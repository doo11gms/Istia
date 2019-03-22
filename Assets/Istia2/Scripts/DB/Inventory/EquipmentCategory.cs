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
    [CreateAssetMenu(fileName = "EquipmentCategory", menuName = "Istia2/DB/EquipmentCategory")]
    public class EquipmentCategory : SerializedScriptableObject, Save.ISavable
    {
        void Save.ISavable.Save()
        {
            ES2.Save(Name, GetInstanceID() + "Name");
        }

        void Save.ISavable.Load()
        {
            Name = ES2.Load<string>(GetInstanceID() + "Name");
        }

        [OdinSerialize] public string Name { get; set; }
    }
}