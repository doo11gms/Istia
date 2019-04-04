using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.DB
{
    [CreateAssetMenu(menuName = "Istia4/Provider/ExpTableProvider", fileName = "ExpTableProvider")]
    public class ExpTableProvider : SerializedScriptableObject
    {
        [OdinSerialize] public Dictionary<int, long> ExpTable { get; set; } = new Dictionary<int, long>();

        public Dictionary<int, long> Provide() => ExpTable;
    }
}
