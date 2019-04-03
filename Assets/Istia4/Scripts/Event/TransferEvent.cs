using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.Event
{
    public class TransferEvent : EventBase
    {
        [Title("Required")]
        [OdinSerialize, Required] Profile.SystemProfile SystemProfile;

        [Title("Transfer to")]
        [OdinSerialize] Meta.Location Location { get; set; }

        protected override void Execute()
        {
            if (Location.scene == null) throw new System.Exception("遷移先のシーンが指定されていません。");

            var playerStatus = (GameSystem.Actor.Player.PlayerStatus)FindObjectOfType(typeof(GameSystem.Actor.Player.PlayerStatus));
            playerStatus.StartLocation = new Meta.Location(Location.scene, Location.position, Location.eulerAngles);
            UnityEngine.SceneManagement.SceneManager.LoadScene(SystemProfile.MapLoadScene);
        }
    }
}