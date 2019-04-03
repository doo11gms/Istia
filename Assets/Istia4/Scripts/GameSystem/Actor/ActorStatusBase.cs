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
    public abstract class ActorStatusBase : SerializedMonoBehaviour, Save.ISavable
    {
        void Save.ISavable.Save()
        {
        }

        void Save.ISavable.Load()
        {
        }

        [Title("Basic")]
        [OdinSerialize] public string Name { get; private set; }

        [Title("Avator")]
        [OdinSerialize] public DB.Avator Avator { get; private set; }

        [Title("Spec")]
        [OdinSerialize] public Dictionary<DB.Parameter, long> ParameterValues { get; private set; } = new Dictionary<DB.Parameter, long>();
    }
}