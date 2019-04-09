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
    public struct ShortcutInfo
    {
        public Sprite IconSprite { get; set; }
        public string TargetID { get; set; }
        public SHORTCUT_TYPE ShortcutType { get; set; }

        public ShortcutInfo(Sprite iconSprite, string targetID, SHORTCUT_TYPE shortcutType)
        {
            IconSprite = iconSprite;
            TargetID = targetID;
            ShortcutType = shortcutType;
        }
    }
}