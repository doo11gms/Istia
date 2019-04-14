using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.GameSystem.Actor.Mob.Behaviour
{
    /// <summary>
    /// アクターの行動を安全に制御するエントリを提供します。
    /// </summary>
    public class BehaviourController : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] Transform Mob { get; set; }
        [OdinSerialize, Required] Chase Chase { get; set; }
        [OdinSerialize, Required] Wander Wander { get; set; }
        //[OdinSerialize, Required] Die m_Die;
        //[OdinSerialize, Required] Ressurect m_Ressurect;

        public void Stop()
        {
            Chase.Stop();
            Wander.Stop();
        }

        public void AllowMove()
        {
            Chase.gameObject.SetActive(true);
            Wander.gameObject.SetActive(true);
        }

        public void DisallowMove()
        {
            Chase.gameObject.SetActive(false);
            Wander.gameObject.SetActive(false);
        }

        public void Teleport(Vector3? position = null, Vector3? eulerAngles = null)
        {
            if (position != null) Mob.position = (Vector3)position;
            if (eulerAngles != null) Mob.eulerAngles = (Vector3)eulerAngles;
            Stop();
        }

        [Title("Buttons")]
        [Button("Die")]
        public void Die()
        {
            //m_WayPointMove.gameObject.SetActive(false);
            //m_Die.gameObject.SetActive(true);
        }

        [Button("Ressurect")]
        public void Ressurect()
        {
            //m_Ressurect.gameObject.SetActive(true);
            //m_WayPointMove.gameObject.SetActive(true);
        }
    }
}