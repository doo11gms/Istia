using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.GameSystem.Map
{
    public class MapLoadManager : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] Profile.PlayerProfile PlayerProfile { get; set; }

        [Title("Settings")]
        [OdinSerialize] string PlayerTag { get; set; } = "PlayerTag";

        void MoveToNextScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(PlayerProfile.StartLocation.scene);
        }

        private void Start()
        {
            Actor.Behaviour.BehaviourController playerBehaviourController = null;

            playerBehaviourController.Stop();
            // teleport 

            MoveToNextScene();
        }
    }
}