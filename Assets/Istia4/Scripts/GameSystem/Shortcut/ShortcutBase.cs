using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.GameSystem.Shortcut
{
    public abstract class ShortcutBase : SerializedMonoBehaviour
    {
        [OdinSerialize] public string ContentID { get; protected set; }

        public abstract void Run();
    }
}