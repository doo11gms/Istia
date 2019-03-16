using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia.GameSystem
{
    public class WindowHandler : SerializedMonoBehaviour
    {
        [Button("Open")]
	    public void Open(UI.Window.WindowBase window)
        {
            window.Activate();
        }

        [Button("Close")]
        public void Close(UI.Window.WindowBase window)
        {
            window.Deactivate();
        }
    }
}