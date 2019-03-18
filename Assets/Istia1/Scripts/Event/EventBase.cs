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
    public abstract class EventBase : SerializedMonoBehaviour
    {
        [OdinSerialize] bool DeactivateOnExecuted = true;

        private void OnEnable()
        {
            Execute();
            if (DeactivateOnExecuted) gameObject.SetActive(false);
        }

        protected abstract void Execute();
    }
}