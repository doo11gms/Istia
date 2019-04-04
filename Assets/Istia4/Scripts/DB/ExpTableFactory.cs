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
    [CreateAssetMenu(menuName = "Istia4/Factory/ExpTableFactory", fileName = "ExpTableFactory")]
    public class ExpTableFactory : SerializedScriptableObject
    {
        [Title("Danger")]
        [OdinSerialize] bool EnableParameterEditing { get; set; } = false;

        [Title("Parameter")]
        [OdinSerialize, EnableIf("EnableParameterEditing")] long DefaultExp { get; set; } = 30;
        [OdinSerialize, EnableIf("EnableParameterEditing")] float MinAlpha { get; set; } = 1.3f;
        [OdinSerialize, EnableIf("EnableParameterEditing")] float MaxAlpha { get; set; } = 1.8f;
        [OdinSerialize, EnableIf("EnableParameterEditing")] int LevelGap { get; set; } = 100;
        [OdinSerialize, EnableIf("EnableParameterEditing")] AnimationCurve AnimationCurve { get; set; }

        // 下記のメソッドを使って累積経験値を一発で計算しようとすると、StackOverFlowが発生します。
        // これを回避するために回りくどい処理を行っています。
        //long CalculateAccExp(int level)
        //{
        //    if (level < 1) throw new System.Exception();
        //    if (level == 1) return 0;
        //    if (level == 2) return DefaultExp + CalculateAccExp(level--);

        //    float Alpha() => Mathf.Clamp(1 + (1 - level / StepsCount), MinAlpha, MaxAlpha);

        //    return (long)(CalculateAccExp(level--) * Alpha());
        //}

        float Alpha(int level)
        {
            if (level >= LevelGap) return MinAlpha;
            return Mathf.Clamp(1 + AnimationCurve.Evaluate((float)level / LevelGap), MinAlpha, MaxAlpha);
        }

        long NextAccExp(int level, long currentAccExp)
        {
            if (level <= 0) throw new System.Exception();
            if (level == 1) return DefaultExp;
            return (long)(currentAccExp * Alpha(level));
        }

        public Dictionary<int, long> CreateExpTable(int maxLevel = 150)
        {
            var table = new Dictionary<int, long>();
            long accExp = 0;
            for(int level = 1; level <= maxLevel; level++)
            {
                if (level > 1) accExp = NextAccExp(level - 1, accExp);
                table.Add(level, accExp);
            }
            return table;
        }
    }
}
