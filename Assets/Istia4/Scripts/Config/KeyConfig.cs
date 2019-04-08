using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.Config
{
    public static class KeyConfig
    {
        [Title("General")]
        public static KeyCode CancelKey = KeyCode.Escape;

        [Title("Window")]
        public static KeyCode InventoryWindowKey = KeyCode.I;
        public static KeyCode EquipmentWindowKey = KeyCode.E;
        public static KeyCode HotKeyWindowKey = KeyCode.H;

        [Title("Inventory")]
        public static int UseItemMouseButton = 0;
        public static KeyCode DisposeItemAllKey = KeyCode.LeftAlt;
        public static int DisposeItemAllMouseButton = 1;
        public static int EquipMouseButton = 1;
        public static KeyCode ItemLootingKey = KeyCode.Space;

        [Title("Shortcut")]
        public static int UseShortcutMouseButton = 0;
        public static int UnassignShortcutMouseButton = 1;

        [Title("Player Move")]
        public static int PlayerMoveMouseButton = 0;
    }
}