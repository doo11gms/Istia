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
        [OdinSerialize] public string ID { get; private set; }
        [OdinSerialize] public string Name { get; private set; }

        /// <summary>
        /// 転送先のマナトンネルIDです。
        /// </summary>
        [OdinSerialize] public string Destination { get; private set; }

        public void SetDestination(ManaTunnel destination)
        {
            Destination = destination.ID;
        }

        public void SetDestination(string id)
        {
            Destination = id;
        }

        public void Unassign()
        {
            Destination = null;
        }

        public void Transport()
        {
            if (Destination == null)
            {
                Debug.Log("このマナトンネルには転送先が登録されていません。");
            }
            else
            {

            }
        }
    }
}