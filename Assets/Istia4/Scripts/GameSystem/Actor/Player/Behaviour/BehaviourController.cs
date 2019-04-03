using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.GameSystem.Actor.Player.Behaviour
{
    /// <summary>
    /// アクターの行動を安全に制御するエントリを提供します。
    /// </summary>
    public class BehaviourController : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] Transform m_Player;
        [OdinSerialize, Required] WayPointMove m_WayPointMove;
        //[OdinSerialize, Required] Die m_Die;
        //[OdinSerialize, Required] Ressurect m_Ressurect;

        public void Stop()
        {
            m_WayPointMove.Stop();
        }

        public void AllowMove()
        {
            m_WayPointMove.gameObject.SetActive(true);
        }

        public void DisallowMove()
        {
            m_WayPointMove.gameObject.SetActive(false);
        }

        public void Teleport(Vector3? position = null, Vector3? eulerAngles = null)
        {
            if (position != null) m_Player.position = (Vector3)position;
            if (eulerAngles != null) m_Player.eulerAngles = (Vector3)eulerAngles;
            Stop();
        }

        [Title("Buttons")]
        [Button("Die")]
        public void Die()
        {
            //m_WayPointMove.gameObject.SetActive(false);
           // m_Die.gameObject.SetActive(true);
        }

        [Button("Ressurect")]
        public void Ressurect()
        {
            //m_Ressurect.gameObject.SetActive(true);
            //m_WayPointMove.gameObject.SetActive(true);
        }
    }
}