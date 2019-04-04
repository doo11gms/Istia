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
            Save.SaveHandler.Save(this, m_Name, nameof(m_Name));
        }

        void Save.ISavable.Load()
        {
            Save.SaveHandler.Load(this, ref m_Name, nameof(m_Name));
        }

        [TitleGroup("Basic")]
        [OdinSerialize] protected DB.Status DefaultStatus { get; set; }

        [TitleGroup("Basic")]
        [OdinSerialize] string m_Name;
        public string Name
        {
            get { return m_Name; }
        }

        [Title("Avator")]
        [OdinSerialize] public DB.Avator Avator { get; private set; }

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