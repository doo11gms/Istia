using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.Profile
{
    [CreateAssetMenu(menuName = "Istia1/Profile/PlayerProfile", fileName = "PlayerProfile")]
    public class PlayerProfile : SerializedScriptableObject, Save.ISavable
    {
        void Save.ISavable.Save()
        {
            if (SpecFactors == null) throw new System.Exception("SpecFactorsがnullであるため、セーブできません。");
            ES2.Save(Name, GetInstanceID() + "Name");
            ES2.Save(SpecFactors, GetInstanceID() + "SpecFactors");
        }

        void Save.ISavable.Load()
        {
            if (SpecFactors == null) throw new System.Exception("SpecFactorsがnullであるため、ロードできません。");
            Name = ES2.Load<string>(GetInstanceID() + "Name");
            SpecFactors = ES2.LoadDictionary<DB.SpecFactorType, long>(GetInstanceID() + "SpecFactors");
        }

        [Title("Basic")]
        [OdinSerialize] public string Name { get; set; }

        [Title("Avator")]
        [OdinSerialize] public DB.Avator Avator { get; set; }

        [Title("Spec")]
        [OdinSerialize] public Dictionary<DB.SpecFactorType, long> SpecFactors { get; set; } = new Dictionary<DB.SpecFactorType, long>();
    }
}