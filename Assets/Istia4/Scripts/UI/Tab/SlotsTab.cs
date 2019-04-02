using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.UI.Tab
{
    public class SlotsTab : TabBase
    {
        [OdinSerialize] public List<Slot.SlotBase> Slots { get; set; } = new List<Slot.SlotBase>();
    }
}