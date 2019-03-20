using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.GameSystem.Actor.Behaviour.Move
{
    public abstract class MoveBase : SerializedMonoBehaviour
    {
        public abstract void Stop();
        public abstract void AllowMove();
        public abstract void DisallowMove();

	    protected virtual void Awake()
        {

        }

        protected virtual void OnEnable()
        {

        }

        protected virtual void Update()
        {

        }

        protected virtual void FixedUpdate()
        {

        }
    }
}