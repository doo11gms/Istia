using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.Event
{
    public class SceneChangeEvent : EventBase
    {
        [Title("Settings")]
        [OdinSerialize] string NextSceneName { get; set; }

        [Title("Fade Out")]
        [OdinSerialize] bool UsingFadeOut { get; set; } = true;
        [OdinSerialize, EnableIf("UsingFadeOut")] float Duration { get; set; } = 1f;

        protected override void Execute()
        {
            if (NextSceneName == null) throw new System.Exception("遷移先のシーンが指定されていません。");

            if (UsingFadeOut)
            {
                Debug.Log("TODO");
                //Gui.FadeManager.Instance.LoadScene(NextSceneName, Duration);
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(NextSceneName);
            }
        }
    }
}