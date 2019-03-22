using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia2.Save
{
    [CreateAssetMenu(menuName = "Istia2/Save/SaveHandler", fileName = "SaveHandler")]
    public class SaveHandler : SerializedScriptableObject
    {
        [OdinSerialize] List<ISavable> m_ISavables = new List<ISavable>();

        [Button("Save")]
	    public void Save()
        {
            if (m_ISavables == null) return;
            m_ISavables.ForEach(savable => savable.Save());
            Debug.Log("[SaveHandler]Saved.");
        }

        [Button("Load")]
        public void Load()
        {
            if (m_ISavables == null) return;
            m_ISavables.ForEach(savable => savable.Load());
            Debug.Log("[SaveHandler]Loaded.");
        }

        [Button("Delete")]
        public void Delete()
        {
            ES2.DeleteDefaultFolder();
            Debug.Log("[SaveHandler]Deleted.");
        }
    }
}