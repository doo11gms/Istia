using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.Profile
{
    [CreateAssetMenu(menuName = "Istia1/Profile/InventoryProfile", fileName = "InventoryProfile")]
    public class InventoryProfile : SerializedScriptableObject, Save.ISavable
    {
        void Save.ISavable.Save()
        {
            // TODO
        }

        void Save.ISavable.Load()
        {
            // TODO
        }

        [System.Serializable]
        struct Content
        {
            public int tabID;
            public int slotID;
            public string identifier;
            public int count;

            public Content(int tabID, int slotID, string identifier, int count)
            {
                this.tabID = tabID;
                this.slotID = slotID;
                this.identifier = identifier;
                this.count = count;
            }
        }

        [Title("State")]
        [OdinSerialize] List<Content> Contents { get; set; } = new List<Content>();

        Content? Search(int tabID, int slotID)
        {
            if (Contents == null) return null;

            foreach (var content in Contents)
            {
                if (content.tabID == tabID && content.slotID == slotID) return content;
            }

            return null;
        }

        bool Exists(int tabID, int slotID)
        {
            if (Contents == null) return false;

            return Search(tabID, slotID) != null;
        }

        int? SearchIndex(Content content)
        {
            if (Contents == null) return null;

            var index = 0;

            foreach (var found in Contents)
            {
                if (Equals(found, content)) return index;
                index++;
            }

            return null;
        }

        public string GetIdentifier(int tabID, int slotID)
        {
            if (Contents == null) return null;

            var found = Search(tabID, slotID);

            if (found == null)
            {
                return null;
            }
            else
            {
                return ((Content)found).identifier;
            }
        }

        public int? GetCount(int tabID, int slotID)
        {
            if (Contents == null) return null;

            var found = Search(tabID, slotID);

            if (found == null)
            {
                return null;
            }
            else
            {
                return ((Content)found).count;
            }
        }

        public List<int> GetChildSlotIDs(int tabID)
        {
            var ids = new List<int>();

            Contents.ForEach(content =>
            {
                if (content.tabID == tabID) ids.Add(content.slotID);
            });

            return ids;
        }

        [Button("Initialize")]
        public void Initialize()
        {
            Contents = new List<Content>();
        }

        [Button("Assign")]
        public void Assign(int tabID, int slotID, string identifier, int count = 1)
        {
            if (Contents == null) Initialize();

            var assigner = new Content(tabID, slotID, identifier, count);
            var found = Search(tabID, slotID);

            if (found == null)
            {
                Contents.Add(assigner);
            }
            else
            {
                Contents[(int)SearchIndex((Content)found)] = assigner;
            }
        }

        [Button("Unassign")]
        public bool Unassign(int tabID, int slotID)
        {
            if (Contents == null) return false;

            if (Exists(tabID, slotID))
            {
                Contents.Remove((Content)Search(tabID, slotID));
                return true;
            }

            return false;
        }
    }
}