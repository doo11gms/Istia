using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.Config
{
    [CreateAssetMenu(menuName = "Istia1/Config/KeyConfig", fileName = "KeyConfig")]
    public class KeyConfig : SerializedScriptableObject
    {
        [Title("Window")]
        [SerializeField] public KeyCode InventoryWindowKey = KeyCode.I;

        [Title("Inventory")]
        [SerializeField] public int UseItemMouseButton = 0;
        [SerializeField] public KeyCode DisposeItemAllKey = KeyCode.LeftAlt;
        [SerializeField] public int DisposeItemAllMouseButton = 0;
        [SerializeField] public int EquipMouseButton = 1;
    }
}