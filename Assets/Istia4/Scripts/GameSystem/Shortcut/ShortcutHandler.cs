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
    [CreateAssetMenu(fileName = "ShortcutHandler", menuName = "Istia4/Handler/ShortcutHandler")]
    public class ShortcutHandler : SerializedScriptableObject
    {
        public void UseItemShortcut(DB.Inventory.ItemInfo itemInfo)
        {
            var handler = FindObjectOfType<Actor.Player.InventoryHandler>();
            if (handler == null) throw new System.Exception("InventoryHandlerが見つかりません。");
            handler.FindAndUse(itemInfo);
        }

        public void UseSkillShortcut(DB.SkillInfo skillInfo)
        {
            Debug.LogError("TODO");
        }
    }
}