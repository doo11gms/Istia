using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.GameSystem.Item
{
    public abstract class ItemUsingEffectBase : SerializedMonoBehaviour
    {
        private void OnEnable()
        {
            Execute();
        }

        protected abstract void Execute();
    }
}