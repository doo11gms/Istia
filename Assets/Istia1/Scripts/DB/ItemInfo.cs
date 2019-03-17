using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.DB
{
    [CreateAssetMenu(menuName = "Istia1/DB/ItemInfo")]
    public class ItemInfo : SerializedScriptableObject
    {
        [Title("Graphic")]
        [OdinSerialize, PreviewField] public Sprite IconSprite { get; set; }

        [Title("Meta")]
        [OdinSerialize] public string ItemID { get; set; }

        [Title("Basic")]
        [OdinSerialize] public string ItemName { get; set; }

        [Title("Stack")]
        [OdinSerialize] public int MaxStackCount { get; set; } = 99;

        [Title("Usage")]
        [OdinSerialize] public bool Usable { get; set; } = true;
        [OdinSerialize] public bool Consumable { get; set; } = true;

        [Title("Equip")]
        [OdinSerialize] public bool Equipable { get; set; } = false;

        [Title("Trade")]
        [OdinSerialize] public bool Sellable { get; set; } = true;
        [OdinSerialize] public uint BuyingPrice { get; set; } = 10;
        [OdinSerialize] public uint SellingPrice { get; set; } = 10;

        [Title("Disposal")]
        [OdinSerialize] public bool Disposable { get; set; } = true;

        [Title("Hot Key")]
        [OdinSerialize] public bool UsingHotKey { get; set; } = false;
        [OdinSerialize, EnableIf("UsingHotKey")] public KeyCode HotKey { get; set; } = KeyCode.None;

        [Title("Cool Time")]
        [OdinSerialize] public bool UsingCoolTime { get; set; } = false;
        [OdinSerialize, EnableIf("UsingCoolTime")] public float CoolTime { get; set; }
    }
}