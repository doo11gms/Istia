using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using EllGames.Istia4.Extension;

namespace EllGames.Istia4.GameSystem.Actor.Mob.Behaviour
{
    public class Wander : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] Animator Animator { get; set; }
        [OdinSerialize, Required] NavMeshAgent NavMeshAgent { get; set; }

        [Title("Settings")]
        [OdinSerialize] float Interval { get; set; } = 3f;
        [OdinSerialize] string AnimationName { get; set; } = "Walk";

        const float DESTINATION_DISTANCE = 1000f;

        Vector3 RandomDirection()
        {
            var x = Random.Range(-1f, 1f);
            var z = Random.Range(-1f, 1f);
            return new Vector3(x, 0f, z).normalized;
        }

        public void Stop()
        {
            NavMeshAgent.destination = NavMeshAgent.transform.position;
            Animator.SetBool(AnimationName, false);
        }

        private void OnEnable()
        {
            StartCoroutine(DestinationUpdateCoroutine());
        }

        private void OnDisable()
        {
            Stop();
            StopCoroutine(DestinationUpdateCoroutine());
        }

        private void Update()
        {
        }

        IEnumerator DestinationUpdateCoroutine()
        {
            while (true)
            {
                NavMeshAgent.destination = NavMeshAgent.transform.position;
                Animator.SetBool(AnimationName, false);

                yield return new WaitForSeconds(Interval);

                var destination = NavMeshAgent.transform.position;
                destination += (RandomDirection() * DESTINATION_DISTANCE);
                NavMeshAgent.destination = destination;
                Animator.SetBool(AnimationName, true);

                Debug.Log(NavMeshAgent.destination);

                yield return new WaitForSeconds(Interval);
            }
        }
    }
}