using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.GameSystem.Actor
{
    public abstract class StatusBase : SerializedMonoBehaviour
    {
        [TitleGroup("Basic")]
        [OdinSerialize] protected DB.Status DefaultStatus { get; set; }

        [OdinSerialize, HideInEditorMode, InfoBox("These values will be automatically updated per frame, so your editing them by the inspector is no meaning.")] public Dictionary<DB.Parameter, long> CurrentParameterValues { get; protected set; } = new Dictionary<DB.Parameter, long>();

        protected virtual void Update()
        {
            foreach (var key in DefaultStatus.ParameterValues.Keys)
            {
                CurrentParameterValues[key] = DefaultStatus.ParameterValues[key];
            }
        }
    }
}