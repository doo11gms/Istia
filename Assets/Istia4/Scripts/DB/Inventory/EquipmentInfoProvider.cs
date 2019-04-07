using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.DB.Inventory
{
    [CreateAssetMenu(fileName = "EquipmentInfoProvider", menuName = "Istia4/Provider/EquipmentInfoProvider")]
    public class EquipmentInfoProvider : SerializedScriptableObject
    {
        [OdinSerialize] List<EquipmentInfo> Providables { get; set; } = new List<EquipmentInfo>();

        EquipmentInfo Search(string equipmentInfoID)
        {
            foreach (var found in Providables)
            {
                if (found.ID == equipmentInfoID) return found;
            }

            return null;
        }

        public EquipmentInfo Provide(string equipmentInfoID)
        {
            var found = Search(equipmentInfoID);
            if (found == null) throw new System.Exception("EquipmentInfoIDに対応するEquipmentInfoが見つかりません。");
            return found;
        }
    }
}