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
    public class MoveManager : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] WayPointMove WayPointMove { get; set; }
        [OdinSerialize, Required] TeleportMove TeleportMove { get; set; }

        List<MoveBase> m_LatestAllowedMoves = new List<MoveBase>();



        public void ReallowMove()
        {

        }

        public void DisallowMove()
        {
        }

        public void Teleport(Vector3? position = null, Vector3? eulerAngles = null)
        {
            TeleportMove.Teleport(position, eulerAngles);
            WayPointMove.Stop();
        }
    }
}