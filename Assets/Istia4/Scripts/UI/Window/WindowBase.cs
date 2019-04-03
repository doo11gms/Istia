using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.UI.Window
{
    public abstract class WindowBase : SerializedMonoBehaviour
    {
        [Button("Open")]
        public void Open()
        {
            gameObject.SetActive(true);
        }

        [Button("Close")]
        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void FlipOpen()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}