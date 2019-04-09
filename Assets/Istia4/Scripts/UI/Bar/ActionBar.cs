using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.UI.Bar
{
    public class ActionBar : BarBase, Save.ISavable
    {
        void Save.ISavable.Save()
        {
            ShortcutSlost.ForEach(slots =>
            {
                slots.ForEach(slot =>
                {
                    ((Save.ISavable)slot).Save();
                });
            });
        }

        void Save.ISavable.Load()
        {
            ShortcutSlost.ForEach(slots =>
            {
                slots.ForEach(slot =>
                {
                    ((Save.ISavable)slot).Load();
                });
            });
        }

        [OdinSerialize] List<List<Slot.ShortcutSlot>> ShortcutSlost { get; set; } = new List<List<Slot.ShortcutSlot>>();
    }
}