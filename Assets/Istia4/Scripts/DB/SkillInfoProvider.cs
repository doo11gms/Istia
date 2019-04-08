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
    [CreateAssetMenu(fileName = "SkillInfoProvider", menuName = "Istia4/Provider/SkillInfoProvider")]
    public class SkillInfoProvider : SerializedScriptableObject
    {
        [OdinSerialize] List<SkillInfo> Providables { get; set; } = new List<SkillInfo>();

        SkillInfo Search(string skillInfoID)
        {
            foreach (var found in Providables)
            {
                if (found.ID == skillInfoID) return found;
            }

            return null;
        }

        public SkillInfo Provide(string skillInfoID)
        {
            var found = Search(skillInfoID);
            if (found == null) throw new System.Exception("SkillInfoIDに対応するSkillInfoが見つかりません。");
            return found;
        }
    }
}