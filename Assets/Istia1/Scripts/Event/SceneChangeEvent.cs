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
        [OdinSerialize] string Scene { get; set; }

        protected override void Execute()
        {
            if (Scene == null) throw new System.Exception("遷移先のシーンが指定されていません。");
            UnityEngine.SceneManagement.SceneManager.LoadScene(Scene);
        }
    }
}