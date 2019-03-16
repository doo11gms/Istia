using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia.Config
{
    [CreateAssetMenu(menuName = "Config/KeyConfig")]
    public class KeyConfig : SerializedScriptableObject
    {
        [SerializeField] public int UseItemMouseButton = 0;
        [SerializeField] public KeyCode DisposeAllItemTriggerKey = KeyCode.LeftAlt;
        [SerializeField] public int DisposeAllItemTriggerMouseButton = 0;
        [SerializeField] public int OpenItemMenuMouseButton = 1;
    }
}