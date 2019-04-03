using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.UI.Window
{
    public class WindowManager : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] InventoryWindow InventoryWindow { get; set; }
        [OdinSerialize, Required] EquipmentWindow EquipmentWindow { get; set; }

        private void Update()
        {
            if (Input.GetKeyDown(Config.KeyConfig.InventoryWindowKey))
            {
                InventoryWindow.FlipOpen();
            }

            if (Input.GetKeyDown(Config.KeyConfig.EquipmentWindowKey))
            {
                EquipmentWindow.FlipOpen();
            }

            if (Input.GetKeyDown(Config.KeyConfig.CancelKey))
            {
                InventoryWindow.Close();
                EquipmentWindow.Close();
            }
        }
    }
}