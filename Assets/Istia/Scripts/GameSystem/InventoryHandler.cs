using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia.GameSystem
{
    public class InventoryHandler : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] UI.Window.InventoryWindow m_InventoryWindow;
        [OdinSerialize, Required] Item.ItemCoolTimeHandler m_ItemCoolTimeHandler;

        UI.Tab.ItemSlotsTab ParentTab(DB.ItemInfo itemInfo)
        {
            if (itemInfo.Consumable) return m_InventoryWindow.ConsumableItemTab;

            return null;
        }

        /// <summary>
        /// アイテムを使用します。
        /// </summary>
        /// <param name="itemSlot"></param>
        public bool UseItem(UI.Slot.ItemSlot itemSlot)
        {
            if (itemSlot.ContentInfo.UsingCoolTime)
            {
                if (!m_ItemCoolTimeHandler.CoolTimeHasFinished(itemSlot.ContentInfo))
                {
                    Debug.Log("クールタイム中のため、使用できません。");
                    return false;
                }
            }

            m_ItemCoolTimeHandler.Assign(itemSlot.ContentInfo);
            Debug.Assert(itemSlot.ContentInfo.ItemUsingEffects != null);
            itemSlot.ContentInfo.ItemUsingEffects.ForEach(effect => effect.Execute());
            itemSlot.Dispose();

            return true;
        }

        /// <summary>
        /// インベントリにアイテムを1つ追加します。
        /// </summary>
        /// <param name="itemInfo"></param>
        [Button("Push Item")]
	    public bool PushItem(DB.ItemInfo itemInfo)
        {
            var tab = ParentTab(itemInfo);

            if (tab == null) throw new System.Exception("このアイテムを格納できるタブが存在しません。");

            foreach(var slot in tab.Contents)
            {
                if (slot.Push(itemInfo)) return true;
            }

            return false;
        }

        /// <summary>
        /// スロット内のアイテムを1つ破棄します。
        /// </summary>
        /// <param name="itemSlot"></param>
        public void DisposeItem(UI.Slot.ItemSlot itemSlot)
        {
            itemSlot.Dispose();
        }

        /// <summary>
        /// スロット内のアイテムを全て破棄します。
        /// インベントリやタブ内のアイテムを全て破棄する訳ではありません。
        /// </summary>
        /// <param name="itemSlot"></param>
        public void DisposeAllItem(UI.Slot.ItemSlot itemSlot)
        {
            itemSlot.DisposeAll();
        }
    }
}