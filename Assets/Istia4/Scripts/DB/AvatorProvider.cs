using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.DB
{
    [CreateAssetMenu(menuName = "Istia4/Provider/AvatorProvider", fileName = "AvatorProvider")]
    public class AvatorProvider : SerializedScriptableObject
    {
        [OdinSerialize] public List<Avator> Providables { get; set; } = new List<Avator>();

        public Avator Provide(string id)
        {
            foreach(var avator in Providables)
            {
                if (avator.ID == id) return avator;
            }

            throw new System.Exception("IDに対応するアバターが見つかりません。");
        }
    }
}
