﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using EllGames.Istia1.Extension;

namespace EllGames.Istia1.Utility
{
    public class FadeManager : MonoBehaviour
    {
        enum FADE_TYPE
        {
            FadeIn,
            FadeOut
        }

        static FadeManager m_Instance;

        public static FadeManager Instance
        {
            get
            {
                if (m_Instance == null) m_Instance = (FadeManager)FindObjectOfType(typeof(FadeManager));
                return m_Instance;
            }
        }

        [Title("Settings")]
        [OdinSerialize] public Color DefaultFadeColor { get; set; } = Color.black;
        [OdinSerialize] public float DefaultFadeDuration { get; set; } = 1f;

        Color m_FadeColor;
        bool m_RenderingIsNeeded;

        public void FadeIn(Color? color = null, float? duration = null)
        {
            Fade(FADE_TYPE.FadeIn, color, duration);
        }

        public void FadeOut(Color? color = null, float? duration = null)
        {
            Fade(FADE_TYPE.FadeOut, color, duration);
        }

        void Fade(FADE_TYPE type, Color? color = null, float? duration = null)
        {
            m_FadeColor = color == null ? DefaultFadeColor : (Color)color;
            StartCoroutine(FadeCoroutine(duration == null ? DefaultFadeDuration : (float)duration, type));
        }

        void DrawRect()
        {
            GUI.color = m_FadeColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.blackTexture);
        }

        private void Awake()
        {
            if (m_Instance == this)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnGUI()
        {
            if (m_RenderingIsNeeded) DrawRect();
        }

        IEnumerator FadeCoroutine(float duration, FADE_TYPE type)
        {
            m_RenderingIsNeeded = false;

            var timeElapsed = 0f;

            while(timeElapsed <= duration)
            {
                m_FadeColor.SetAlpha(CalculateAlpha());
                yield return null;
            }

            m_RenderingIsNeeded = true;

            float CalculateAlpha()
            {
                switch (type)
                {
                    default:
                        return 0f;
                    case FADE_TYPE.FadeIn:
                        return 1f - timeElapsed / duration;
                    case FADE_TYPE.FadeOut:
                        return timeElapsed / duration;
                }
            }
        }
    }
}