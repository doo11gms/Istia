using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace EllGames.Istia1.Editor
{
    public class ManaTunnelWindow : EditorWindow
    {
        // メニューからウィンドウを表示
        [MenuItem("Window/Istia1/ManaTunnelWindow")]
        public static void Open()
        {
            GetWindow<ManaTunnelWindow>(typeof(ManaTunnelWindow));
        }

        // ウィンドウを開いた時等に実行
        private void OnEnable()
        {

        }

        private void OnGUI()
        {
            string manaTunnelName;
            string sceneName;
            Vector3 position;
            Vector3 eulerAngles;
        }
    }
}