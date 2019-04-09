using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.UI.Container
{
    public class ShortcutInfoContainer : ContainerBase
    {
        [Title("Required")]
        [OdinSerialize, Required] Image Image { get; set; }

        [Title("Content")]
        [OdinSerialize, ReadOnly] public GameSystem.Shortcut.ShortcutInfo ShortcutInfo { get; private set; }

        bool m_IsOnEnableFrame = false;

        public ShortcutInfoContainer(GameSystem.Shortcut.ShortcutInfo shortcutInfo)
        {
            ShortcutInfo = shortcutInfo;
        }

        public void Assign(GameSystem.Shortcut.ShortcutInfo shortcutInfo)
        {
            ShortcutInfo = shortcutInfo;
            Image.sprite = shortcutInfo.IconSprite;
        }

        public void Unassign()
        {
            ShortcutInfo = null;
            Image.sprite = null;
        }

        public GameSystem.Shortcut.ShortcutInfo TakeOut()
        {
            var buf = ShortcutInfo;
            Unassign();
            if (buf == null) throw new System.Exception("参照が消えています。");
            return buf;
        }

        void MouseTrack()
        {
            transform.position = UnityEngine.Input.mousePosition;
        }

        private void Awake()
        {
            Image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            MouseTrack();
            m_IsOnEnableFrame = true;
        }

        private void Update()
        {
            MouseTrack();
        }

        private void LateUpdate()
        {
            if (Input.GetMouseButtonDown(1) && !m_IsOnEnableFrame) gameObject.SetActive(false);

            m_IsOnEnableFrame = false;
        }
    }
}