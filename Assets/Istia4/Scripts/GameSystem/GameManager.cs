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

namespace EllGames.Istia4.GameSystem
{
    public class GameManager : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] Profile.SystemProfile SystemProfile { get; set; }

        [Title("Settings")]
        [OdinSerialize] List<GameObject> DontDestroysInMapScene { get; set; } = new List<GameObject>();

        bool IsMapScene(Scene scene)
        {
            if (SystemProfile.MapScenePrefixes == null) return false;

            foreach (var prefix in SystemProfile.MapScenePrefixes)
            {
                if (scene.name.IndexOf(prefix) == 0) return true;
            }

            return false;
        }

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
            try
            {
                FindObjectOfType<Save.SaveHandler>().Load();
            }
            catch
            {
                Debug.Log("ロード可能なセーブデータが見つからなかったため、初期データで実行します。");
            }

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