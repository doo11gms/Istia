using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia.Save
{
    [CreateAssetMenu(menuName = "Save/SaveHandler")]
    public class SaveHandler : SerializedScriptableObject
    {
        [OdinSerialize] List<ISavable> m_ISavables = new List<ISavable>();

        [Button("Save")]
	    public void Save()
        {
            if (m_ISavables == null) return;
            m_ISavables.ForEach(savable => savable.Save());
        }

        [Button("Load")]
        public void Load()
        {
            if (m_ISavables == null) return;
            m_ISavables.ForEach(savable => savable.Load());
        }

        [Button("Delete")]
        public void Delete()
        {
            ES2.DeleteDefaultFolder();
        }
    }
}