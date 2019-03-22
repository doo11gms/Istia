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
    [CreateAssetMenu(fileName = "EnchantInfo", menuName = "Istia2/DB/EnchantInfo")]
    public class EnchantInfo : SerializedScriptableObject
    {
        [OdinSerialize] public string Name { get; private set; }
        [OdinSerialize] public List<EquipmentCategory> EnchantableCategories { get; private set; } = new List<EquipmentCategory>();
    }
}