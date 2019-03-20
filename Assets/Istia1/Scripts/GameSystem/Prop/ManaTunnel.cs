using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.GameSystem.Prop
{
    public class ManaTunnel : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] Profile.ManaTunnelProfile ManaTunnelProfile { get; set; }

        [Title("Settings")]
        [OdinSerialize] DB.ManaTunnelInfo SelfInfo { get; set; }
        [OdinSerialize] DB.ManaTunnelInfo DestinationInfo { get; set; }

        [Title("Game Object Reference")]
        [OdinSerialize] GameObject ManaFlames { get; set; }

        [Title("Read Only")]
        [OdinSerialize, ReadOnly] const float FADE_DURATION = 1f;
        [OdinSerialize, ReadOnly] const string PLAYER_TAG = "Player";

        [Button("Transport")]
        public bool Transport()
        {
            if (ManaTunnelProfile.IsLocked(SelfInfo)) return false;

            if (DestinationInfo == null)
            {
                Debug.Log("このマナトンネルには転送先が登録されていません。");
                return false;
            }
            else
            {
                Gui.FadeManager.Instance.LoadScene(DestinationInfo.SceneName, FADE_DURATION);

                var player = GameObject.FindWithTag(PLAYER_TAG);
                player.transform.position = DestinationInfo.Point;
                player.transform.eulerAngles = DestinationInfo.EulerAngles;
            }

            return true;
        }

        [Button("Lock")]
        public void Lock()
        {
            ManaTunnelProfile.Lock(SelfInfo);
            ManaFlames.gameObject.SetActive(false);
        }

        [Button("Unlock")]
        public void Unlock()
        {
            ManaTunnelProfile.Unlock(SelfInfo);
            ManaFlames.gameObject.SetActive(true);
        }
    }
}