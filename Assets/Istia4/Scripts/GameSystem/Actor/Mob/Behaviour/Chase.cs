using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.GameSystem.Actor.Mob.Behaviour
{
    public class Chase : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] public NavMeshAgent NavMeshAgent { get; set; }
        [OdinSerialize, Required] Animator Animator { get; set; }

        [Title("Settings")]
        [OdinSerialize] string AnimationName { get; set; } = "Run";

        Transform Target { get; set; }

        public void Stop()
        {
            Animator.SetBool(AnimationName, false);
            NavMeshAgent.destination = NavMeshAgent.transform.position;
        }

        private void Update()
        {
            if (Target == null)
            {
                Stop();
            }
            else
            {
                Animator.SetBool(AnimationName, true);
                NavMeshAgent.destination = Target.position;
            }
        }
    }
}