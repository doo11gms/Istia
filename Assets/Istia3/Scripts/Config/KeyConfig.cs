using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia3.Config
{
    public static class KeyConfig
    {
        [Title("Window")]
        public static KeyCode InventoryWindowKey = KeyCode.I;

        [Title("Inventory")]
        public static int UseItemMouseButton = 0;
        public static KeyCode DisposeItemAllKey = KeyCode.LeftAlt;
        public static int DisposeItemAllMouseButton = 0;
        public static int EquipMouseButton = 1;

        [Title("Player Move")]
        public static int PlayerMoveMouseButton = 0;
    }
}