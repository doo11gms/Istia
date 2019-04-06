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
        [TitleGroup("Basic")]
        [PropertyOrder(13)]
        [OdinSerialize] public DB.Inventory.ItemCategory ItemCategory { get; set; }

        [TitleGroup("Basic")]
        [PropertyOrder(14)]
        [OdinSerialize, TextArea(2, 2)] string m_Description;
        public string Description
        {
            get { return m_Description; }
        }

        [Title("Stack")]
        [PropertyOrder(21)]
        [OdinSerialize] public int MaxStackCount { get; set; } = 99;

        [Title("Trade")]
        [PropertyOrder(22)]
        [OdinSerialize] public bool Sellable { get; set; } = true;

        [PropertyOrder(23)]
        [OdinSerialize] public uint BuyingPrice { get; set; } = 10;

        [PropertyOrder(24)]
        [OdinSerialize] public uint SellingPrice { get; set; } = 10;

        [Title("Usage")]
        [PropertyOrder(25)]
        [OdinSerialize] public bool Usable { get; set; } = true;

        [PropertyOrder(26)]
        [OdinSerialize, EnableIf("Usable")] public bool Consumable { get; set; } = true;

        [PropertyOrder(27)]
        [SerializeField, EnableIf("Usable")] public List<GameSystem.Item.ItemUsingEffectBase> ItemUsingEffects = new List<GameSystem.Item.ItemUsingEffectBase>();

        [Title("Cool Time")]
        [PropertyOrder(28)]
        [OdinSerialize] public bool UsingCoolTime { get; set; } = false;

        [PropertyOrder(29)]
        [OdinSerialize, EnableIf("UsingCoolTime")] public float CoolTime { get; set; }
    }
}