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
        [OdinSerialize, Required] public NavMeshAgent NavMeshAgent { get; set; }

        public void Stop()
        {

        }
    }
}