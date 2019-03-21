using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.UI.Slot
{
    public class ItemSlot : SlotBase, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
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
            if (UnityEngine.Input.GetMouseButton(KeyConfig.EquipMouseButton))
            {
                // TODO: 装備する
                return;
            }
            else
            {
                PressOverlay.gameObject.SetActive(true);
            }

            if (UnityEngine.Input.GetMouseButton(KeyConfig.DisposeItemAllMouseButton) && UnityEngine.Input.GetKey(KeyConfig.DisposeItemAllKey))
            {
                InventoryHandler.DisposeItemAll(ParentTab.TabID, SlotID);
                Refresh();
                return;
            }

            if (UnityEngine.Input.GetMouseButton(KeyConfig.UseItemMouseButton))
            {
                InventoryHandler.UseItem(ParentTab.TabID, SlotID);
                Refresh();
                return;
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            PressOverlay.gameObject.SetActive(false);
        }

        [Title("Required")]
        [OdinSerialize, Required] Config.KeyConfig KeyConfig { get; set; }
        [OdinSerialize, Required] GameSystem.Actor.Player.InventoryHandler InventoryHandler { get; set; }
        [OdinSerialize, Required] GameSystem.Item.ItemCoolDownHandler ItemCoolDownHandler { get; set; }

        [Title("UI Reference")]
        [OdinSerialize, Required] Tab.ItemSlotsTab ParentTab { get; set; }
        [OdinSerialize, Required] Image IconImage { get; set; }
        [OdinSerialize, Required] Image CoolDownOverlay { get; set; }
        [OdinSerialize, Required] Image HoverOverlay { get; set; }
        [OdinSerialize, Required] Image PressOverlay { get; set; }
        [OdinSerialize, Required] Text CountText { get; set; }

        [Title("Settings")]
        [OdinSerialize] bool AutoRefresh { get; set; } = true;

        [Title("State")]
        [OdinSerialize] DB.ItemInfo Content { get; set; }
        [OdinSerialize] int ContentCount { get; set; }

        public bool IsEmpty()
        {
            return Content == null;
        }

        [Title("Buttons")]

        [Button("Assign")]
        public void Assign(DB.ItemInfo itemInfo, int count)
        {
            Content = itemInfo;
            ContentCount = count;
        }

        [Button("Unassign")]
        public void Unassign()
        {
            Content = null;
            ContentCount = 0;
        }

        /// <summary>
        /// UIを最新の状態に更新します。
        /// </summary>
        [Button("Refresh")]
        public void Refresh()
        {
            if (Content == null)
            {
                IconImage.sprite = null;
                IconImage.gameObject.SetActive(false);
                CoolDownOverlay.gameObject.SetActive(false);
                HoverOverlay.gameObject.SetActive(false);
                PressOverlay.gameObject.SetActive(false);
                CountText.gameObject.SetActive(false);
            }
            else
            {
                IconImage.sprite = Content.IconSprite;
                IconImage.gameObject.SetActive(true);

                if (Content.UsingCoolTime)
                {
                    CoolDownOverlay.fillAmount = ItemCoolDownHandler.CoolTimeRemain(Content);
                    CoolDownOverlay.gameObject.SetActive(true);
                }

                if (ContentCount > 1)
                {
                    CountText.text = ContentCount.ToString();
                    CountText.gameObject.SetActive(true);
                }
                else
                {
                    CountText.gameObject.SetActive(false);
                }
            }
        }

        [Button("Initialize")]
        public void Initialize()
        {
            Unassign();
            Refresh();
        }

        private void Update()
        {
            if (AutoRefresh) Refresh();
        }
    }
}