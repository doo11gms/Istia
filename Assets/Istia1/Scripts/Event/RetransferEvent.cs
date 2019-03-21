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
    public class RetransferEvent : EventBase
    {
        const string MAP_LOAD_SCENE_NAME = "SystemMap_MapLoad";

        protected override void Execute()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(MAP_LOAD_SCENE_NAME);
        }
    }
}