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
    public class InventoryWindow : WindowBase, Save.ISavable
    {
        [Button("Save")]
        void Save.ISavable.Save()
        {
            ConsumableItemTab.Contents.ForEach(slot => (slot as Save.ISavable).Save());
        }

        [Button("Load")]
        void Save.ISavable.Load()
        {
            ConsumableItemTab.Contents.ForEach(slot => (slot as Save.ISavable).Load());
        }

        [Title("Required")]
        [OdinSerialize, Required] public Tab.ItemSlotsTab ConsumableItemTab { get; private set; }
    }
}