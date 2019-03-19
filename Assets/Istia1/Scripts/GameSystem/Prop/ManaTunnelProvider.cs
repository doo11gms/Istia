using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.GameSystem.Prop
{
    [CreateAssetMenu(menuName = "Istia1/Provider/ManaTunnelProvider", fileName = "ManaTunnelProvider")]
    public class ManaTunnelProvider : SerializedScriptableObject
    {
        [OdinSerialize] List<ManaTunnel> ManaTunnels { get; set; } = new List<ManaTunnel>();
        
        public bool Register(ManaTunnel target)
        {
            if (ManaTunnels.Contains(target)) return false;
            ManaTunnels.Add(target);
            return true;
        }

	    public ManaTunnel Provide(string id)
        {
            if (ManaTunnels == null) return null;

            foreach(var found in ManaTunnels)
            {
                if (found.ID == id) return found;
            }

            return null;
        }
    }
}