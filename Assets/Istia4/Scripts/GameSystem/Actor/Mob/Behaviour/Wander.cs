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
        enum STATE
        {
            Idle,
            Wandering
        }

        [Title("Required")]
        [OdinSerialize, Required] Animator Animator { get; set; }
        [OdinSerialize, Required] NavMeshAgent NavMeshAgent { get; set; }

        [Title("Settings")]
        [OdinSerialize] float Interval { get; set; } = 3f;
        [OdinSerialize] string AnimationName { get; set; } = "Walk";

        [Title("State")]
        [OdinSerialize] STATE State { get; set; } = STATE.Idle;

        const float DESTINATION_DISTANCE = 1f;
        Vector3 Direction { get; set; }

        Vector3 RandomDirection()
        {
            var x = Random.Range(-1f, 1f);
            var z = Random.Range(-1f, 1f);
            return new Vector3(x, 0f, z).normalized;
        }

        Vector3 Destination()
        {
            switch (State)
            {
                case STATE.Idle:
                    return NavMeshAgent.transform.position;
                case STATE.Wandering:
                    return NavMeshAgent.transform.position + Direction * DESTINATION_DISTANCE;
            }

            throw new System.Exception("Destination is undefined.");
        }

        void DestinationUpdate()
        {
            NavMeshAgent.destination = Destination();
        }

        void AnimationUpdate()
        {
            switch (State)
            {
                case STATE.Idle:
                    Animator.SetBool(AnimationName, false);
                    break;
                case STATE.Wandering:
                    Animator.SetBool(AnimationName, true);
                    break;
            }
        }

        public void Stop()
        {
            Animator.SetBool(AnimationName, false);
            State = STATE.Idle;
        }

        private void OnEnable()
        {
            StartCoroutine(StateUpdateCoroutine());
        }

        private void OnDisable()
        {
            Stop();
            StopCoroutine(StateUpdateCoroutine());
        }

        private void Update()
        {
            AnimationUpdate();
            DestinationUpdate();
        }

        IEnumerator StateUpdateCoroutine()
        {
            while (true)
            {
                State = STATE.Idle;

                yield return new WaitForSeconds(Interval);

                State = STATE.Wandering;
                Direction = RandomDirection();

                yield return new WaitForSeconds(Interval);
            }
        }
    }
}