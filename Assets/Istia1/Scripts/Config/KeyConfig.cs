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
        [SerializeField] public int UseItemMouseButton = 0;
        [SerializeField] public KeyCode DisposeAllItemKey = KeyCode.LeftAlt;
        [SerializeField] public int DisposeAllItemMouseButton = 0;
        [SerializeField] public int OpenItemMenuMouseButton = 1;
        [SerializeField] public KeyCode InventoryWindowKey = KeyCode.I;
    }
}