using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.GameSystem.Shortcut
{
    public class ShortcutInfo : SerializedMonoBehaviour
    {
        [OdinSerialize] Sprite m_IconSprite;
        public Sprite IconSprite
        {
            get { return m_IconSprite; }
        }

        [OdinSerialize] string m_TargetID;
        public string TargetID
        {
            get { return m_TargetID; }
        }

        [OdinSerialize] SHORTCUT_TYPE m_ShortcutType;
        public SHORTCUT_TYPE ShortcutType
        {
            get { return m_ShortcutType; }
        }

        public ShortcutInfo(Sprite iconSprite, string targetID, SHORTCUT_TYPE shortcutType)
        {
            m_IconSprite = iconSprite;
            m_TargetID = targetID;
            m_ShortcutType = shortcutType;
        }
    }
}