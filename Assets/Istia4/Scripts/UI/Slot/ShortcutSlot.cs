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
            if (IsEmpty()) return;
            PressOverlay.gameObject.SetActive(true);

            if (UnityEngine.Input.GetMouseButton(Config.KeyConfig.UseShortcutMouseButton))
            {
                switch (m_ShortcutType)
                {
                    case SHORTCUT_TYPE.None:
                        return;
                    case SHORTCUT_TYPE.UseItemShortcut:
                        ShortcutHandler.UseItemShortcut(ItemInfo);
                        return;
                    case SHORTCUT_TYPE.UseSkillShortcut:
                        ShortcutHandler.UseSkillShortcut(SkillInfo);
                        return;
                }
            }

            if (UnityEngine.Input.GetMouseButton(Config.KeyConfig.UnassignShortcutMouseButton))
            {
                Unassign();
                return;
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            PressOverlay.gameObject.SetActive(false);
        }

        void Save.ISavable.Save()
        {
            ES2.Save(m_ShortcutType, GetInstanceID() + nameof(m_ShortcutType));
            Save.SaveHandler.Save(this, ContentID, nameof(ContentID));
        }

        void Save.ISavable.Load()
        {
            m_ShortcutType = ES2.Load<SHORTCUT_TYPE>(GetInstanceID() + nameof(m_ShortcutType));
            Save.SaveHandler.Load(this, ref m_ContentID, nameof(ContentID));
            Refresh();
        }

        [Title("Required")]
        [OdinSerialize, Required] DB.Inventory.ItemInfoProvider ItemInfoProvider { get; set; }
        [OdinSerialize, Required] DB.SkillInfoProvider SkillInfoProvider { get; set; }
        [OdinSerialize, Required] GameSystem.Shortcut.ShortcutHandler ShortcutHandler { get; set; }

        [Title("State")]
        [OdinSerialize, ReadOnly] SHORTCUT_TYPE m_ShortcutType = SHORTCUT_TYPE.None;
        public SHORTCUT_TYPE ShortcutType
        {
            get { return m_ShortcutType; }
        }

        [TitleGroup("Content")]
        [OdinSerialize, ReadOnly] string m_ContentID;
        public string ContentID
        {
            get { return m_ContentID; }
        }

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

        void UpdateIcon()
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

        public void Assign(DB.Inventory.ItemInfo itemInfo)
        {
            Unassign();
            m_ShortcutType = SHORTCUT_TYPE.UseItemShortcut;
            m_ContentID = itemInfo.ID;
            ItemInfo = itemInfo;
            UpdateIcon();
        }

        public void Assign(DB.SkillInfo skillInfo)
        {
            Unassign();
            m_ShortcutType = SHORTCUT_TYPE.UseSkillShortcut;
            m_ContentID = skillInfo.ID;
            SkillInfo = skillInfo;
            UpdateIcon();
        }

        public void Unassign()
        {
            m_ShortcutType = SHORTCUT_TYPE.None;
            m_ContentID = null;
            ItemInfo = null;
            SkillInfo = null;
            UpdateIcon();
        }

        public void Refresh()
        {
            Unassign();

            if (m_ContentID != null)
            {
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

            UpdateIcon();
        }
    }
}