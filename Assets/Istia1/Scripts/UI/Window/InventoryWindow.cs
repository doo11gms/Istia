using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.UI.Window
{
    public class InventoryWindow : WindowBase
    {
        [OdinSerialize] List<Tab.ItemSlotsTab> Tabs;

        public Tab.ItemSlotsTab GetTab(int tabID)
        {
            foreach(var tab in Tabs)
            {
                if (tab.TabID == tabID) return tab;
            }

            return null;
        }

        public Slot.ItemSlot GetItemSlot(int tabID, int slotID)
        {
            var tab = GetTab(tabID);

            if (tab == null) return null;

            foreach(var slot in tab.Contents)
            {
                if (slot.SlotID == slotID) return slot;
            }
                 
            return null;
        }

        /// <summary>
        /// UIを最新の状態に更新します。
        /// </summary>
        public void Refresh()
        {
            Tabs.ForEach(tab => tab.Contents.ForEach(slot => slot.Refresh()));
        }

        public void Initialize()
        {
            Tabs.ForEach(tab => tab.Contents.ForEach(slot => slot.Initialize()));
        }

        protected override void Awake()
        {
            base.Awake();
        }
    }
}