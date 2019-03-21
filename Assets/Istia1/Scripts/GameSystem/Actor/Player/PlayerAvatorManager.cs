using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.GameSystem.Actor.Player
{
    public class PlayerAvatorManager : SerializedMonoBehaviour
    {
	    [OdinSerialize, Required] Profile.PlayerProfile PlayerProfile { get; set; }

        DB.Avator CurrentAvatar { get; set; }
        GameObject CurrentModel { get; set; }
        public Animator Animator { get; private set; }

        private void Update()
        {
            var nextAvatar = PlayerProfile.Avator;

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