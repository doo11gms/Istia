using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using EllGames.Istia1.Extension;

namespace EllGames.Istia1.GameSystem.Actor.Behaviour.Move
{
    public class WayPointMove : MoveBase
    {
        [Title("Required")]
        [OdinSerialize, Required] CharacterController CharacterController { get; set; }
        [OdinSerialize, Required] Profile.PlayerProfile PlayerProfile { get; set; }
        [OdinSerialize, Required] Config.KeyConfig KeyConfig { get; set; }
        [OdinSerialize, Required] DB.SpecFactorType SpecFactorTypeOfSpeed { get; set; }

        [Title("Settings")]
        [OdinSerialize, InfoBox("Multiplying this value by Speed becomes Stopping Distance.")] float StoppingDistanceRate { get; set; } = 0.03f;
        [OdinSerialize] float SpeedMag { get; set; } = 0.01f;

        [Title("Raycast")]
        [OdinSerialize] Camera RaycastCamera { get; set; }
        [OdinSerialize] List<string> IgnoreLayers = new List<string>();

        [Title("Animation")]
        [OdinSerialize] bool UsingAnimation { get; set; } = false;
        [OdinSerialize, EnableIf("UsingAnimation")] PlayerAvatorManager PlayerAvatorManager { get; set; }
        [OdinSerialize, EnableIf("UsingAnimation")] string AnimationName { get; set; }
        [OdinSerialize, EnableIf("UsingAnimation")] string AnimationMultiplierName { get; set; }
        [OdinSerialize, EnableIf("UsingAnimation")] float AnimationSpeedMag { get; set; } = 0.1f;

        [Title("State")]
        [OdinSerialize, ReadOnly] bool Movable { get; set; } = true;

        Vector3 Destination { get; set; }

        void DestinationUpdate()
        {
            if (IgnoreLayers == null) IgnoreLayers = new List<string>();
            var layerMask = ~LayerMask.GetMask(IgnoreLayers.ToArray());
            var ray = RaycastCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            var hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) Destination = hit.point;
            else SetDestinationToSelf();
        }

        Vector3 MoveDirection() => ((Destination - CharacterController.transform.position)).normalized;
        float Speed() => PlayerProfile.SpecValues[SpecFactorTypeOfSpeed] * SpeedMag;
        Vector3 CurrentPoint() => CharacterController.transform.position;
        float StoppingDistance() => StoppingDistanceRate * Speed();
        float AnimationSpeed() => AnimationSpeedMag * Speed();

        void SetDestinationToSelf()
        {
            Destination = CharacterController.transform.position;
        }

        bool CloseToDestination()
        {
            // SimpleMoveはy方向のスピードを無視するので、水平距離で比較します。
            var moveTo = CharacterController.transform.position + MoveDirection() * Speed() * Time.deltaTime;
            if (Vector3.Distance((Destination).Flatten(), CharacterController.transform.position.Flatten()) <= Vector3.Distance((Destination).Flatten(), moveTo.Flatten())) return false;
            if (Vector3.Distance(CurrentPoint().Flatten(), Destination.Flatten()) <= StoppingDistance()) return false;

            CharacterController.SimpleMove(MoveDirection() * Speed());

            return true;
        }

        void LookAtDestination()
        {
            var lookedAt = Destination;
            lookedAt.y = CharacterController.transform.position.y;
            CharacterController.transform.LookAt(lookedAt);
        }

        protected override void Awake()
        {
            base.Awake();

            SetDestinationToSelf();
        }

        protected override void Update()
        {
            base.Update();

            if (!Movable) return;

            if (UnityEngine.Input.GetMouseButton(KeyConfig.PlayerMoveMouseButton) &&
                !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) DestinationUpdate();

            if (CloseToDestination())
            {
                LookAtDestination();
                if (UsingAnimation && PlayerAvatorManager.Animator != null)
                {
                    PlayerAvatorManager.Animator.SetBool(AnimationName, true);
                    PlayerAvatorManager.Animator.SetFloat(AnimationMultiplierName, AnimationSpeed());
                }
            }
            else
            {
                if (UsingAnimation && PlayerAvatorManager.Animator != null) PlayerAvatorManager.Animator.SetBool(AnimationName, false);
            }
        }

        #region Buttons

        [Title("Buttons")]

        [Button("Stop")]
        public override void Stop()
        {
            SetDestinationToSelf();
            if (UsingAnimation) PlayerAvatorManager.Animator.SetBool(AnimationName, false);
        }

        [Button("Allow Move")]
        public override void AllowMove()
        {
            Movable = true;
        }

        [Button("Disallow Move")]
        public override void DisallowMove()
        {
            Stop();
            Movable = false;
        }

        #endregion
    }
}
