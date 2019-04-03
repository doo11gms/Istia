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
        [Title("Transfer to")]
        [OdinSerialize] string Scene { get; set; }
        [OdinSerialize] public Vector3 Position { get; set; }
        [OdinSerialize] public Vector3 EulerAngles { get; set; }

        const string MAP_LOAD_SCENE_NAME = "SystemMap_MapLoad";

        protected override void Execute()
        {
            if (Scene == null) throw new System.Exception("遷移先のシーンが指定されていません。");

            var playerStatus = (GameSystem.Actor.Player.PlayerStatus)FindObjectOfType(typeof(GameSystem.Actor.Player.PlayerStatus));
            playerStatus.StartLocation = new Meta.Location(Scene, Position, EulerAngles);
            UnityEngine.SceneManagement.SceneManager.LoadScene(MAP_LOAD_SCENE_NAME);
        }
    }
}