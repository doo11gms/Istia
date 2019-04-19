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
        [Title("Required")]
        [OdinSerialize, Required] DB.ParameterProvider ParameterProvider { get; set; }

        [OdinSerialize, Required] Behaviour.BehaviourController BehaviourController { get; set; }
        [OdinSerialize, Required] MobStatus MobStatus { get; set; }

        DB.Parameter HP { get; set; }
        DB.Parameter MaxHP { get; set; }

        private void Awake()
        {
            HP = ParameterProvider.Provide("HP");
            MaxHP = ParameterProvider.Provide("最大HP");
        }

        private void Update()
        {
            if (MobStatus.CurrentParameterValues[HP] < MobStatus.CurrentParameterValues[MaxHP])
            {
                BehaviourController.AllowChase();
                Debug.Log("TODO: chase and attack");
            }
            else
            {
                BehaviourController.AllowWander();
            }
        }
    }
}