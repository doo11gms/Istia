using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia.UI.Tab
{
    public class ItemSlotsTab : TabBase
    {
	    [OdinSerialize] public List<Slot.ItemSlot> Contents { get; private set; }
    }
}