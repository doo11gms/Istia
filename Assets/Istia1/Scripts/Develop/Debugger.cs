using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine.Windows.Speech;

namespace EllGames.Istia1.Develop
{
    public class Debugger : SerializedMonoBehaviour
    {
        KeywordRecognizer m_KeywordRecognizer;

        private void Start()
        {
            var keywords = new string[]{ "じゃんぷ", "いきろ", "ふぁいと" };
            m_KeywordRecognizer = new KeywordRecognizer(keywords);
            m_KeywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
            m_KeywordRecognizer.Start();
        }

        void OnPhraseRecognized(PhraseRecognizedEventArgs args)
        {
            switch (args.text)
            {
                case "じゃんぷ":
                    Debug.Log("ジャンプします！");
                    break;
                case "いきろ":
                    Debug.Log("生きます！");
                    break;
                case "ふぁいと":
                    Debug.Log("頑張ります！");
                    break;
            }
        }
    }
}