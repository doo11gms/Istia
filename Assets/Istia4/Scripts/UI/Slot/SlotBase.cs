using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.UI.Slot
{
    public abstract class SlotBase : SerializedMonoBehaviour
    {
        [TitleGroup("Meta")]
        [OdinSerialize] public int SlotID { get; private set; }
    }
}