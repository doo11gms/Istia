using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.GameSystem.Actor
{
    public class AvatorManager : SerializedMonoBehaviour
    {
        [OdinSerialize, Required] ActorStatusBase ActorStatus { get; set; } 

        DB.Avator CurrentAvatar { get; set; }
        GameObject CurrentModel { get; set; }
        public Animator Animator { get; private set; }

        private void Update()
        {
            var nextAvatar = ActorStatus.Avator;

            if (CurrentAvatar != nextAvatar)
            {
                var buffer = CurrentModel;
                CurrentAvatar = nextAvatar;
                CurrentModel = Instantiate(nextAvatar.Model, transform);
                Animator = CurrentModel.GetComponent<Animator>();
                Destroy(buffer);
            }
        }
    }
}