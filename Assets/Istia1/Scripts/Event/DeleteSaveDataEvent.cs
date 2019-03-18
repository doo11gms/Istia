using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.Event
{
    public class DeleteSaveDataEvent : EventBase
    {
        [Title("Required")]
        [OdinSerialize, Required] Save.SaveHandler SaveHandler { get; set; }

        protected override void Execute()
        {
            Debug.Assert(SaveHandler != null);
            SaveHandler.Delete();
        }
    }
}