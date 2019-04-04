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
        [OdinSerialize, Required] DB.AvatorProvider AvatorProvider { get; set; }
        [OdinSerialize, Required] StatusBase Status { get; set; } 

        string CurrentAvatorID { get; set; }
        GameObject CurrentModel { get; set; }
        public Animator Animator { get; private set; }

        private void Update()
        {
            var nextAvatorID = Status.AvatorID;

            if (CurrentAvatorID != nextAvatorID)
            {
                var destroyTarget = CurrentModel;
                CurrentAvatorID = nextAvatorID;
                CurrentModel = Instantiate(AvatorProvider.Provide(nextAvatorID).Model, transform);
                Animator = CurrentModel.GetComponent<Animator>();
                Destroy(destroyTarget);
            }
        }
    }
}