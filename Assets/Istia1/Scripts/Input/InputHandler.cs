using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.Input
{
    public class InputHandler : SerializedMonoBehaviour
    {
        [Title("Required")]
	    [OdinSerialize, Required] Config.KeyConfig KeyConfig { get; set; }

        [Title("Reference")]
        [OdinSerialize] UI.Window.InventoryWindow InventoryWindow { get; set; }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyConfig.InventoryWindowKey))
            {
                InventoryWindow.FlipOpen();
            }
        }
    }
}