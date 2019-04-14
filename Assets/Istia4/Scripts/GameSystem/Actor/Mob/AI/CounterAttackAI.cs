using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.GameSystem.Actor.Mob.AI
{
    public class CounterAttackAI : AIBase
    {
        [OdinSerialize, Required] Behaviour.BehaviourController BehaviourController { get; set; }
        [OdinSerialize, Required] MobStatus MobStatus { get; set; }
        [OdinSerialize] DB.Parameter HPParameter { get; set; }
        [OdinSerialize] DB.Parameter MaxHPParameter { get; set; }
        // ↑こうやるんじゃなくてプロバイダに変えた方がいい

        private void Update()
        {
            if (MobStatus.CurrentParameterValues[HPParameter] < MobStatus.CurrentParameterValues[MaxHPParameter])
            {
                Debug.Log("TODO: chase and attack");
            }
            else
            {
                Debug.Log("TODO: wander");
            }
        }
    }
}