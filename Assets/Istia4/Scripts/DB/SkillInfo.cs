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
    [CreateAssetMenu(fileName = "SkillInfo", menuName = "Istia4/DB/SkillInfo")]
    public class SkillInfo : SerializedScriptableObject
    {
        [Title("Graphic")]
        [PropertyOrder(10)]
        [OdinSerialize, PreviewField] public Sprite IconSprite { get; private set; }

        [Title("Meta")]
        [PropertyOrder(11)]
        [OdinSerialize] public string ID { get; set; }

        [TitleGroup("Basic")]
        [PropertyOrder(12)]
        [OdinSerialize] public string Name { get; set; }

        [TitleGroup("Spec")]
        [OdinSerialize] public float Duration { get; set; }
        [OdinSerialize] public float CoolTime { get; set; }
    }
}