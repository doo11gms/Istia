using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.UI.Window
{
    public class InventoryWindow : WindowBase
    {
        [OdinSerialize] public List<UI.Tab.SlotsTab> Tabs { get; private set; } = new List<UI.Tab.SlotsTab>();

        public Tab.SlotsTab SearchTab(int tabID)
        {
            foreach(var tab in Tabs)
            {
                if (tab.TabID == tabID) return tab;
            }

            return null;
        }
    }
}