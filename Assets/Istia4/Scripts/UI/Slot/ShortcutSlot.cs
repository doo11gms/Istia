using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using SHORTCUT_TYPE = EllGames.Istia4.GameSystem.Shortcut.SHORTCUT_TYPE;

namespace EllGames.Istia4.UI.Slot
{
    public class ShortcutSlot : SlotBase, Save.ISavable, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (IsEmpty()) return;
            HoverOverlay.gameObject.SetActive(true);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            HoverOverlay.gameObject.SetActive(false);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (UnityEngine.Input.GetMouseButton(1))
            {
                if (!IsEmpty()) Unassign();
                var container = FindObjectOfType<Container.ShortcutInfoContainer>();
                if (container != null) Assign(container.TakeOut());
                return;
            }
            else
            {
                if (IsEmpty()) return;

                if (UnityEngine.Input.GetMouseButton(0))
                {
                    switch (m_ShortcutType)
                    {
                        case SHORTCUT_TYPE.None:
                            break;
                        case SHORTCUT_TYPE.UseItemShortcut:
                            PressOverlay.gameObject.SetActive(true);
                            ShortcutHandler.UseItemShortcut(ItemInfo);
                            break;
                        case SHORTCUT_TYPE.UseSkillShortcut:
                            PressOverlay.gameObject.SetActive(true);
                            ShortcutHandler.UseSkillShortcut(SkillInfo);
                            break;
                    }
                    return;
                }
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            PressOverlay.gameObject.SetActive(false);
        }

        void Save.ISavable.Save()
        {
            Save.SaveHandler.Save(this, m_ContentID, nameof(m_ContentID));
            ES2.Save(m_IconSprite, GetInstanceID() + nameof(m_IconSprite));
            ES2.Save(m_ShortcutType, GetInstanceID() + nameof(m_ShortcutType));
        }

        void Save.ISavable.Load()
        {
            Save.SaveHandler.Load(this, ref m_ContentID, nameof(m_ContentID));
            m_IconSprite = ES2.Load<Sprite>(GetInstanceID() + nameof(m_IconSprite));
            m_ShortcutType = ES2.Load<SHORTCUT_TYPE>(GetInstanceID() + nameof(m_ShortcutType));
            Refresh();
        }

        [Title("Required")]
        [OdinSerialize, Required] DB.Inventory.ItemInfoProvider ItemInfoProvider { get; set; }
        [OdinSerialize, Required] DB.SkillInfoProvider SkillInfoProvider { get; set; }
        [OdinSerialize, Required] GameSystem.Shortcut.ShortcutHandler ShortcutHandler { get; set; }

        [Title("Loaded Data")]
        [OdinSerialize, ReadOnly] SHORTCUT_TYPE m_ShortcutType = SHORTCUT_TYPE.None;
        [OdinSerialize, ReadOnly] string m_ContentID;
        [OdinSerialize, ReadOnly] Sprite m_IconSprite;

        [TitleGroup("Content")]
        [OdinSerialize, ReadOnly] DB.Inventory.ItemInfo ItemInfo { get; set; }

        [TitleGroup("Content")]
        [OdinSerialize, ReadOnly] DB.SkillInfo SkillInfo { get; set; }

        [Title("UI Reference")]
        [OdinSerialize] Image IconImage { get; set; }
        [OdinSerialize] Image HoverOverlay { get; set; }
        [OdinSerialize] Image PressOverlay { get; set; }

        bool IsEmpty()
        {
            return ItemInfo == null && SkillInfo == null;
        }

        void IconRefresh()
        {
            IconImage.sprite = null;
            switch (m_ShortcutType)
            {
                case SHORTCUT_TYPE.UseItemShortcut:
                    IconImage.sprite = ItemInfo.IconSprite;
                    break;
                case SHORTCUT_TYPE.UseSkillShortcut:
                    IconImage.sprite = SkillInfo.IconSprite;
                    break;
            }
            IconImage.gameObject.SetActive(IconImage.sprite != null);
        }

        void InfoRefresh()
        {
            ItemInfo = null;
            SkillInfo = null;
            switch (m_ShortcutType)
            {
                case SHORTCUT_TYPE.None:
                    break;
                case SHORTCUT_TYPE.UseItemShortcut:
                    ItemInfo = ItemInfoProvider.Provide(m_ContentID);
                    break;
                case SHORTCUT_TYPE.UseSkillShortcut:
                    SkillInfo = SkillInfoProvider.Provide(m_ContentID);
                    break;
            }
        }

        public void Assign(GameSystem.Shortcut.ShortcutInfo shortcutInfo)
        {
            Unassign();
            m_ShortcutType = shortcutInfo.ShortcutType;
            m_ContentID = shortcutInfo.TargetID;
            m_IconSprite = shortcutInfo.IconSprite;
            Refresh();
        }

        public void Unassign()
        {
            m_ShortcutType = SHORTCUT_TYPE.None;
            m_ContentID = null;
            ItemInfo = null;
            SkillInfo = null;
        }

        public void Refresh()
        {
            Unassign();
            IconRefresh();
            InfoRefresh();
        }
    }
}