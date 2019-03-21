using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.GameSystem.Actor.Behaviour
{
    /// <summary>
    /// アクターの行動を安全に制御するエントリを提供します。
    /// </summary>
    public class BehaviourController : SerializedMonoBehaviour
    {
        [OdinSerialize] Move.MoveManager MoveManager { get; set; }

        [Button("Allow Move")]
	    public void AllowMove(Move.MoveBase moveBase)
        {
            DisallowMove();
            //
        }

        [Button("Disallow Move")]
        public void DisallowMove()
        {
            //
        }

        public void Stop()
        {
            Debug.Log("TODO:Stop method");
        }

        public void Teleport(Vector3? position = null, Vector3? eulerAngles = null)
        {
            Debug.Log("TODO:MoveにTeleportを追加");
        }
    }
}