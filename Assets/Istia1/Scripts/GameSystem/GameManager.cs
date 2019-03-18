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

namespace EllGames.Istia1.GameSystem
{
    public class GameManager : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] Config.SystemConfig SystemConfig { get; set; }

        [Title("Settings")]
        [OdinSerialize] List<GameObject> DontDestroysInMapScene { get; set; } = new List<GameObject>();

        bool IsMapScene(Scene scene) => scene.name.IndexOf(SystemConfig.MapScenePrefix) == 0;

        void OnSceneLoaded(Scene loaded, LoadSceneMode mode)
        {
            if (!IsMapScene(loaded))
            {
                DontDestroysInMapScene.ForEach(obj => Destroy(obj));
                Destroy(gameObject);
            }
        }

        void OnSceneUnloaded(Scene unloaded)
        {

        }

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            DontDestroyOnLoad(gameObject);
            DontDestroysInMapScene.ForEach(obj => DontDestroyOnLoad(obj));
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
    }
}