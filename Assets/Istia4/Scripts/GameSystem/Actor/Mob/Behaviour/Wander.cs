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
    public class Wander : SerializedMonoBehaviour
    {
        [OdinSerialize, Required] NavMeshAgent NavMeshAgent { get; set; }
        [OdinSerialize] float Interval { get; set; } = 3f;
        [OdinSerialize, ReadOnly] Vector3 Destination { get; set; }

        public void Stop()
        {

        }

        IEnumerator DestinationUpdateCoroutine()
        {
            yield return new WaitForSeconds(Interval);

        }
    }
}