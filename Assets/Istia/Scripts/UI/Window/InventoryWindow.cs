using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia.UI.Window
{
    public class InventoryWindow : WindowBase
    {
        [Title("Required")]
        [OdinSerialize, Required] public Tab.ItemSlotsTab ConsumableItemTab { get; private set; }
    }
}