using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine.SceneManagement;
using Behaviour = EllGames.Istia1.GameSystem.Actor.Player.Behaviour;

namespace EllGames.Istia1.GameSystem.Map
{
    public class MapLoadManager : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] Profile.PlayerProfile PlayerProfile { get; set; }

        [Title("UI Reference")]
        [OdinSerialize] Slider Slider { get; set; }
        [OdinSerialize] Text Text { get; set; }

        void AdjustPlayerLocation()
        {
            var controller = (Behaviour.BehaviourController)FindObjectOfType(typeof(Behaviour.BehaviourController));

            if (controller == null)
            {
                throw new System.Exception("プレイヤーのBehaviourControllerが取得できませんでした。");
            }
            else
            {
                controller.Teleport(PlayerProfile.StartLocation.position, PlayerProfile.StartLocation.eulerAngles);
            }
        }

        private void Start()
        {
            AdjustPlayerLocation();
            StartCoroutine(LoadSceneAsyncCoroutine(PlayerProfile.StartLocation.scene));
        }

        IEnumerator LoadSceneAsyncCoroutine(string scene)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(scene);

            async.allowSceneActivation = false;

            while (!async.isDone)
            {
                if (async.progress >= 0.9f)
                {
                    Slider.value = Slider.maxValue;
                    Text.gameObject.SetActive(true);

                    if (UnityEngine.Input.anyKeyDown)
                    {
                        async.allowSceneActivation = true;
                        yield return async;
                    }
                }
                else
                {
                    Slider.value = async.progress;
                    Text.gameObject.SetActive(false);
                }

                yield return null;
            }
        }
    }
}