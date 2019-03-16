using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia.UI.Slot
{
    /// <summary>
    /// アイテムを格納するためのスロットです。
    /// </summary>
    public class ItemSlot : SlotBase, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, Save.ISavable
    {
        void Save.ISavable.Save()
        {
            ES2.Save(Count, "testCount");
            var filename = "testIdentifier";
            if (ContentInfo == null)
            {
                ES2.Save("ArienaiID", filename);
            }
            else
            {
                ES2.Save(ContentInfo.Identifier, filename);
            }
        }

        void Save.ISavable.Load()
        {
            Initialize();

            try
            {
                Count = ES2.Load<int>("testCount");

                var content = m_ItemInfoProvider.Search(ES2.Load<string>("testIdentifier"));
                if (content != null)
                {
                    Assign(content);
                }
            }
            catch
            {
                throw new System.Exception("セーブデータの復元に失敗しました。");
            }

            UpdateCountText();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (IsEmpty()) return;
            m_HoverOverlay.gameObject.SetActive(true);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (IsEmpty()) return;
            m_HoverOverlay.gameObject.SetActive(false);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (IsEmpty()) return;

            if (Input.GetMouseButton(m_KeyConfig.OpenItemMenuMouseButton))
            {
                // TODO: 装備する
            }
            else
            {
                m_PressOverlay.gameObject.SetActive(true);
            }

            if (Input.GetMouseButton(m_KeyConfig.DisposeAllItemTriggerMouseButton) && Input.GetKey(m_KeyConfig.DisposeAllItemTriggerKey))
            {
                m_InventoryHandler.DisposeAllItem(this);
                return;
            }

            if (Input.GetMouseButton(m_KeyConfig.UseItemMouseButton))
            {
                m_InventoryHandler.UseItem(this);
                return;
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (IsEmpty()) return;
            m_PressOverlay.gameObject.SetActive(false);
        }

        [Title("Required")]
        [OdinSerialize, Required] Config.KeyConfig m_KeyConfig;
        [OdinSerialize, Required] GameSystem.InventoryHandler m_InventoryHandler;
        [OdinSerialize, Required] GameSystem.Item.ItemCoolTimeHandler m_ItemCoolTimeHandler;
        [OdinSerialize, Required] Provider.ItemInfoProvider m_ItemInfoProvider;

        [Title("UI Reference")]
        [OdinSerialize, Required] Image m_IconImage;
        [OdinSerialize, Required] Image m_CoolTimeOverlay;
        [OdinSerialize, Required] Image m_HoverOverlay;
        [OdinSerialize, Required] Image m_PressOverlay;
        [OdinSerialize, Required] Text m_CountText;

        [Title("State")]
        [OdinSerialize, ReadOnly] public DB.ItemInfo ContentInfo { get; private set; }
        [OdinSerialize, ReadOnly] public int Count { get; private set; }

        void UpdateCountText()
        {
            if (Count < 0) throw new System.Exception("アイテムの格納数が負であるため、所持数の変更に失敗しました。");

            m_CountText.text = Count.ToString();

            if (Count > 1)
            {
                m_CountText.gameObject.SetActive(true);
            }
            else
            {
                m_CountText.gameObject.SetActive(false);
            }
        }

        void Assign(DB.ItemInfo itemInfo)
        {
            if (itemInfo.UsingCoolTime) m_CoolTimeOverlay.gameObject.SetActive(true);
            ContentInfo = itemInfo;
            m_IconImage.sprite = itemInfo.IconSprite;
            m_IconImage.gameObject.SetActive(true);
        }

        void Unassign()
        {
            if (ContentInfo != null)
            {
                if (ContentInfo.UsingCoolTime) m_CoolTimeOverlay.gameObject.SetActive(false);
                ContentInfo = null;
            }

            m_IconImage.sprite = null;
            m_IconImage.gameObject.SetActive(false);
        }

        public bool IsEmpty() => ContentInfo == null;

        private void Update()
        {
            if (IsEmpty()) return;

            if (ContentInfo.UsingCoolTime)
            {
                m_CoolTimeOverlay.fillAmount = m_ItemCoolTimeHandler.CoolTimeRemain(ContentInfo) / ContentInfo.CoolTime;
            }

            if (ContentInfo.UsingHotKey)
            {
                if (Input.GetKeyDown(ContentInfo.HotKey))
                {
                    m_InventoryHandler.UseItem(this);
                }
            }
        }

        [Title("Buttons")]

        /// <summary>
        /// スロットを初期化します。
        /// </summary>
        [Button("Initialize")]
        public void Initialize()
        {
            Unassign();
            Count = 0;
            m_HoverOverlay.gameObject.SetActive(false);
            m_PressOverlay.gameObject.SetActive(false);
            m_CountText.gameObject.SetActive(false);
            m_CoolTimeOverlay.gameObject.SetActive(false);
        }

        /// <summary>
        /// 対象のアイテムをスロットに格納します。
        /// 格納に成功した場合trueを、失敗した場合falsaeを返します。
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="count"></param>
        [Button("Push")]
        public bool Push(DB.ItemInfo itemInfo, int count = 1)
        {
            if (count <= 0) throw new System.Exception("0個以下でプッシュすることはできません。");
            if (itemInfo == null) throw new System.Exception("nullをプッシュすることはできません。");

            if (ContentInfo != null && ContentInfo != itemInfo)
            {
                // このスロットには既に他のアイテムが格納されているため、プッシュできません。
                return false;
            }

            if (Count + count > itemInfo.MaxStackCount)
            {
                // 容量オーバーのため、プッシュできません。
                return false;
            }

            Count += count;
            Assign(itemInfo);
            UpdateCountText();

            return true;
        }

        /// <summary>
        /// アイテムを1つ取り出します。
        /// スロットが空である場合、nullを返します。
        /// </summary>
        [Button("Pop")]
        public DB.ItemInfo Pop()
        {
            if (IsEmpty()) throw new System.Exception("スロットに何も格納されていないため、取り出すことができません。");
            if (Count <= 0) throw new System.Exception("スロットにアイテムが1つも格納されていないため、取り出すことができません。");

            Count--;
            var popped = ContentInfo;
            if (Count == 0) Unassign();
            UpdateCountText();
            return popped;
        }

        /// <summary>
        /// 格納されているアイテムを1つ破棄します。
        /// アイテムの格納数が0の場合、例外が発生します。
        /// </summary>
        [Button("Dispose")]
        public void Dispose()
        {
            if (IsEmpty()) throw new System.Exception("スロットに何も格納されていないため、破棄することができません。");
            if (Count <= 0) throw new System.Exception("スロットにアイテムが1つも格納されていないため、破棄することができません。");

            Count--;
            if (Count == 0) Unassign();
            UpdateCountText();
        }

        /// <summary>
        /// 格納されているアイテムをすべて破棄します。
        /// アイテムの格納数が0の場合でも、例外は発生しません。
        /// </summary>
        [Button("Dispose All")]
        public void DisposeAll()
        {
            if (IsEmpty()) throw new System.Exception("スロットに何も格納されていないため、破棄することができません。");
            if (Count < 0) throw new System.Exception("アイテムの格納数が負であるため、アイテムを破棄できません。");

            Count = 0;
            Unassign();
            UpdateCountText();
        }
    }
}