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
    public abstract class StatusBase : SerializedMonoBehaviour, Save.ISavable
    {
        void Save.ISavable.Save()
        {
        }

        void Save.ISavable.Load()
        {
        }

        [TitleGroup("Required")]
        [OdinSerialize, Required] protected DB.Status DefaultStatus { get; set; }

        [Title("Basic")]
        [OdinSerialize] public string Name { get; private set; }

        [Title("Avator")]
        [OdinSerialize] public DB.Avator Avator { get; private set; }

        [Title("Spec")]
        [OdinSerialize, InfoBox("These values will be automatically updated per frame, so you cannot change them by Inspector.")] public Dictionary<DB.Parameter, long> CurrentParameterValues { get; protected set; } = new Dictionary<DB.Parameter, long>();

        protected virtual void Update()
        {
            foreach (var key in DefaultStatus.ParameterValues.Keys)
            {
                CurrentParameterValues[key] = DefaultStatus.ParameterValues[key];
            }
        }
    }
}