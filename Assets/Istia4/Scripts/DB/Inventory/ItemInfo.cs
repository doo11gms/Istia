using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.DB.Inventory
{
    [CreateAssetMenu(menuName = "Istia4/DB/ItemInfo", fileName = "ItemInfo")]
    public class ItemInfo : InventoryItemInfoBase
    {
        [Title("Meta")]
        [OdinSerialize, PropertyOrder(1)] public string ID { get; set; }

        [Title("Category")]
        [OdinSerialize, PropertyOrder(1)] public DB.Inventory.ItemCategory ItemCategory { get; set; }

        [Title("Basic")]
        [OdinSerialize, PropertyOrder(1)] public string ItemName { get; set; }
        [OdinSerialize, TextArea(2, 2), PropertyOrder(2)] string m_Description;
        public string Description
        {
            get { return m_Description; }
        }

        [Title("Stack")]
        [OdinSerialize, PropertyOrder(3)] public int MaxStackCount { get; set; } = 99;

        [Title("Trade")]
        [OdinSerialize, PropertyOrder(3)] public bool Sellable { get; set; } = true;
        [OdinSerialize, PropertyOrder(3)] public uint BuyingPrice { get; set; } = 10;
        [OdinSerialize, PropertyOrder(3)] public uint SellingPrice { get; set; } = 10;

        [Title("Disposal")]
        [OdinSerialize, PropertyOrder(3)] public bool Disposable { get; set; } = true;

        [Title("Hot Key")]
        [OdinSerialize, PropertyOrder(3)] public bool UsingHotKey { get; set; } = false;
        [OdinSerialize, EnableIf("UsingHotKey"), PropertyOrder(3)] public KeyCode HotKey { get; set; } = KeyCode.None;

        [Title("Cool Time")]
        [OdinSerialize, PropertyOrder(3)] public bool UsingCoolTime { get; set; } = false;
        [OdinSerialize, EnableIf("UsingCoolTime"), PropertyOrder(3)] public float CoolTime { get; set; }

        [Title("Usage")]
        [OdinSerialize, PropertyOrder(4)] public bool Usable { get; set; } = true;
        [OdinSerialize, PropertyOrder(4), EnableIf("Usable")] public bool Consumable { get; set; } = true;
        [SerializeField, PropertyOrder(5), EnableIf("Usable")] public List<GameSystem.Item.ItemUsingEffectBase> ItemUsingEffects = new List<GameSystem.Item.ItemUsingEffectBase>();
    }
}