using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.Event
{
    public class DeleteSaveDataEvent : EventBase
    {
        protected override void Execute()
        {
            FindObjectOfType<Save.SaveHandler>().Delete();
        }
    }
}