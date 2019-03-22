using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia2.GameSystem.Actor.Player
{
    /// <summary>
    /// 装備システムを安全に扱う機能を提供するエントリです。
    /// </summary>
    public class EquipmentHandler : SerializedMonoBehaviour
    {
        [OdinSerialize, Required] Profile.EquipmentProfile EquipmentProfile { get; set; }

        public bool Equip(DB.Inventory.EquipmentInfoBase equipmentInfo)
        {
            return true;
        }

        public bool Unequip()
        {
            return true;
        }
    }
}