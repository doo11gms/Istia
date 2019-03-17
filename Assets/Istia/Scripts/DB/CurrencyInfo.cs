using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia.DB
{
    [CreateAssetMenu(menuName = "Istia/DB/CurrencyInfo")]
    public class CurrencyInfo : SerializedScriptableObject
    {
        [Title("Usage")]
        [OdinSerialize] public string CurrencyName { get; private set; }
        [OdinSerialize] public string Unit { get; private set; }
    }
}