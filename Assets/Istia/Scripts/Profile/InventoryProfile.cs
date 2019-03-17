using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia.Profile
{
    [CreateAssetMenu(menuName = "Istia/Profile/InventoryProfile")]
    public class InventoryProfile : SerializedScriptableObject, Save.ISavable
    {
        struct Content
        {
            public int tabID;
            public int slotID;
            public int count;
            public string identifier;

            public Content(int tabID, int slotID, string identifier, int count)
            {
                this.tabID = tabID;
                this.slotID = slotID;
                this.identifier = identifier;
                this.count = count;
            }
        }

        void Save.ISavable.Save()
        {
            // TODO
        }

        void Save.ISavable.Load()
        {
            // TODO
        }

        [Title("State")]
        [OdinSerialize] List<Content> m_Contents = new List<Content>();

        Content? Search(int tabID, int slotID)
        {
            foreach(var content in m_Contents)
            {
                if (content.tabID == tabID && content.slotID == slotID) return content;
            }

            return null;
        }

        bool Exists(int tabID, int slotID)
        {
            return Search(tabID, slotID) != null;
        }

        int? GetIndex(Content content)
        {
            var index = 0;

            foreach(var found in m_Contents)
            {
                if (Equals(found, content)) return index;
                index++;
            }

            return null;
        }

        [Button("Assign")]
        public void Assign(int tabID, int slotID, string identifier, int count = 1)
        {
            var found = Search(tabID, slotID);
            var content = new Content(tabID, slotID, identifier, count);

            if (found == null)
            {
                m_Contents.Add(content);
            }
            else
            {
                m_Contents[(int)GetIndex((Content)found)] = content;
            }
        }

        [Button("Unassign")]
        public bool Unassign(int tabID, int slotID)
        {
            if (Exists(tabID, slotID))
            {
                m_Contents.Remove((Content)Search(tabID, slotID));
                return true;
            }

            return false;
        }
    }
}