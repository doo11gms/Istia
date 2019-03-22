﻿using System.Collections;
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
    [CreateAssetMenu(fileName = "EquipmentInfo", menuName = "Istia2/DB/EquipmentInfo")]
    public class EquipmentInfo : InventoryItemBase, Save.ISavable
    {
        void Save.ISavable.Save()
        {
            (EquipmentCategory as Save.ISavable).Save();
        }

        void Save.ISavable.Load()
        {
            (EquipmentCategory as Save.ISavable).Load();
        }

        [Title("Category")]
        [OdinSerialize] public EquipmentCategory EquipmentCategory { get; set; }

        [Title("Spec")]
        [OdinSerialize] Dictionary<Parameter, int> ParameterValues { get; set; } = new Dictionary<Parameter, int>();
    }
}