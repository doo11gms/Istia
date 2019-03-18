﻿using System.Collections;
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
    public class ItemSlotsTab : TabBase
    {
        [Title("State")]
        [OdinSerialize] public List<Slot.ItemSlot> Contents { get; set; }
    }
}