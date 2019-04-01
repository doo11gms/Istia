using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.GameSystem.Actor.Player
{
    public class InventoryHandler : SerializedMonoBehaviour
    {
        public bool Push()
        {
            return true;
        }

        public bool Dispose()
        {
            return true;
        }

        public bool DisposeAll()
        {
            return true;
        }
    }
}