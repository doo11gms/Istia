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
        // TODO プレイヤーのモデルが切り替わっても対応できるようにする
        enum STATE
        {
            Idling,
            Moving,
        }

        [Title("Required")]
        [OdinSerialize, Required] CharacterController CharacterController { get; set; }
        [OdinSerialize, Required] Profile.PlayerProfile PlayerProfile { get; set; }
        [OdinSerialize, Required] Config.KeyConfig KeyConfig { get; set; }
        [OdinSerialize, Required] DB.SpecFactorType SpecFactorTypeOfSpeed { get; set; }

        [Title("Settings")]

        [Title("Raycast")]
        [OdinSerialize] Camera RaycastCamera { get; set; }
        [OdinSerialize] List<string> IgnoreLayers = new List<string>();

        [Title("Animation")]
        [OdinSerialize] bool UsingAnimation = false;
        [OdinSerialize, EnableIf("UsingAnimation")] Animator Animator { get; set; }
        [OdinSerialize, EnableIf("UsingAnimation")] string AnimationName { get; set; }

        [Title("Read Only")]
        [OdinSerialize, ReadOnly] STATE State { get; set; }
        [OdinSerialize, ReadOnly] bool Movable { get; set; } = true;
        [OdinSerialize, ReadOnly] Vector3 Destination { get; set; }

        void DestinationUpdate()
        {
            if (IgnoreLayers == null) IgnoreLayers = new List<string>();
            var layerMask = ~LayerMask.GetMask(IgnoreLayers.ToArray());
            var ray = RaycastCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            var hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) Destination = hit.point;
        }

        void StateUpdate()
        {

        }

        void AnimationUpdate()
        {
            switch (State)
            {
                case STATE.Idling:
                    break;
                case STATE.Moving:
                    break;
            }
        }

        Vector3 MoveDirection() => (Destination - CharacterController.transform.position).normalized;

        bool CloseToDestination()
        {
            var speed = PlayerProfile.SpecFactors[SpecFactorTypeOfSpeed];
            speed /= 100;
            var moveTo = CharacterController.transform.position + MoveDirection() * speed * Time.deltaTime;

            float HorizontalDistance(Vector3 a, Vector3 b)
            {
                a.y = 0f;
                b.y = 0f;
                return Vector3.Distance(a, b);
            }

            float error = 0.01f;

            // SimpleMoveはy方向のspeedを無視するので、水平距離で比較します。
            if (Vector3.Distance(Destination.Flatten(), CharacterController.transform.position.Flatten()) <= Vector3.Distance(Destination.Flatten(), moveTo.Flatten()) + error) return false;
            //if (HorizontalDistance(Destination, CharacterController.transform.position) <= HorizontalDistance(Destination, moveTo) + error) return false;

            CharacterController.SimpleMove(MoveDirection() * speed);

            return true;
        }

        protected override void Awake()
        {
            base.Awake();

            Stop();
        }

        protected override void Update()
        {
            base.Update();

            if (!Movable) return;

            if (UnityEngine.Input.GetMouseButton(KeyConfig.PlayerMoveMouseButton) &&
                !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) DestinationUpdate();

            StateUpdate();
            AnimationUpdate();

            CloseToDestination();
            switch (State)
            {
                case STATE.Idling:
                    break;
                case STATE.Moving:
                    break;
            }
        }

        protected override void FixedUpdate()
        {
            //todo:move 
        }

        #region Buttons

        [Title("Buttons")]

        [Button("Stop")]
        public void Stop()
        {
            Destination = CharacterController.transform.position;
        }

        [Button("Allow Move")]
        public void AllowMove()
        {
            Movable = true;
        }

        [Button("Disallow Move")]
        public void DisallowMove()
        {
            Movable = false;
        }

        #endregion
    }
}
