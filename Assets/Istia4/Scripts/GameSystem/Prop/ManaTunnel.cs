using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.GameSystem.Prop
{
    public class ManaTunnel : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] GameObject RetransferEventPrefab { get; set; }
        [OdinSerialize, Required] GameObject ManaFlamesRoot { get; set; }

        [Title("Info")]
        [OdinSerialize] DB.ManaTunnelInfo SelfInfo { get; set; }
        [OdinSerialize] DB.ManaTunnelInfo DestinationInfo { get; set; }

        const string PLAYER_TAG = "Player";
        ManaTunnelManager ManaTunnelManager { get; set; }

        /// <summary>
        /// マナトンネルを起動します。
        /// </summary>
        /// <returns></returns>
        [Button("Boot")]
        public bool Boot()
        {
            if (!ManaTunnelManager.IsActive(SelfInfo)) return false;

            var playerStatus = (Actor.Player.PlayerStatus)FindObjectOfType(typeof(Actor.Player.PlayerStatus));
            var location = DestinationInfo.Location;
            location.position += DestinationInfo.SpawnPositionOffset;
            location.eulerAngles += DestinationInfo.SpawnEulerAnglesOffset;
            playerStatus.StartLocation = location;
            Instantiate(RetransferEventPrefab).gameObject.SetActive(true);

            return true;
        }

        private void Start()
        {
            ManaTunnelManager = FindObjectOfType<ManaTunnelManager>();
            if (ManaTunnelManager == null) throw new System.Exception("ManaTunnelManagerが見つかりません。");
        }

        private void Update()
        {
            ManaFlamesRoot.gameObject.SetActive(ManaTunnelManager.IsActive(SelfInfo));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == PLAYER_TAG) Boot();
        }
    }
}
