using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.GameSystem.Actor.Behaviour.Move
{
    public class TeleportMove : MoveBase
    {
        [System.Serializable]
        struct Location
        {
            public Vector3? position;
            public Vector3? eulerAngles;
        }

        [Title("Required")]
        [OdinSerialize, Required] Transform Target { get; set; }

        [Title("Read Only")]
        [OdinSerialize, ReadOnly] bool Movable { get; set; } = true;

        Location m_Destination;

        public void SetDestination(Vector3? position, Vector3? eulerAngles)
        {
            m_Destination.position = position;
            m_Destination.eulerAngles = eulerAngles;
        }

        public void Teleport()
        {
            if (!Movable) return;
            if (m_Destination.position != null) Target.transform.position = (Vector3)m_Destination.position;
            if (m_Destination.eulerAngles != null) Target.transform.eulerAngles = (Vector3)m_Destination.eulerAngles;
        }

        public void Teleport(Vector3? position, Vector3? eulerAngles)
        {
            SetDestination(position, eulerAngles);
            Teleport();
        }

        public override void AllowMove()
        {
            Movable = true;
        }

        public override void DisallowMove()
        {
            Movable = false;
        }

        public override void Stop() { }
    }
}