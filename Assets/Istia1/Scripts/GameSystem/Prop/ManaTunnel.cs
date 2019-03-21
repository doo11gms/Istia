using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace EllGames.Istia1.GameSystem.Prop
{
    public class ManaTunnel : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] Profile.PlayerProfile PlayerProfile { get; set; }
        [OdinSerialize, Required] Profile.ManaTunnelProfile ManaTunnelProfile { get; set; }

        [Title("Prefab")]
        [OdinSerialize] GameObject RetransferEvent { get; set; }

        [Title("Settings")]
        [OdinSerialize] DB.ManaTunnelInfo SelfInfo { get; set; }
        [OdinSerialize] DB.ManaTunnelInfo DestinationInfo { get; set; }

        [Title("Game Object Reference")]
        [OdinSerialize] GameObject ManaFlamesRoot { get; set; }

        /// <summary>
        /// マナトンネルを起動します。
        /// </summary>
        /// <returns></returns>
        [Button("Boot")]
        public bool Boot()
        {
            if (!ManaTunnelProfile.IsActive(SelfInfo)) return false;

            PlayerProfile.StartLocation = new Profile.PlayerProfile.Location(DestinationInfo.SceneName, DestinationInfo.Point, DestinationInfo.EulerAngles);
            Instantiate(RetransferEvent).gameObject.SetActive(true);

            return true;
        }

        private void Update()
        {
            ManaFlamesRoot.gameObject.SetActive(ManaTunnelProfile.IsActive(SelfInfo));
        }
    }
}
