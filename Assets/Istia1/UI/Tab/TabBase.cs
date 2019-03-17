using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.UI.Tab
{
    public abstract class TabBase : SerializedMonoBehaviour
    {
        [Title("Meta")]
        [OdinSerialize] public int TabID { get; set; }
    }
}