using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.GameSystem.Item
{
    public abstract class ItemUsingEffectBase : SerializedMonoBehaviour
    {
        protected void Awake()
        {
            Execute();
        }

        protected virtual void Execute()
        {
            Destroy(gameObject);
        }
    }
}