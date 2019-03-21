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
        [System.Serializable]
        public struct Location
        {
            public string scene;
            public Vector3 position;
            public Vector3 eulerAngles;
        }

        void Save.ISavable.Save()
        {
            if (SpecValues == null) throw new System.Exception("SpecValuesがnullであるため、セーブできません。");
            ES2.Save(Name, GetInstanceID() + "Name");
            ES2.Save(SpecValues, GetInstanceID() + "SpecValues");
        }

        void Save.ISavable.Load()
        {
            if (SpecValues == null) throw new System.Exception("SpecValuesがnullであるため、ロードできません。");
            Name = ES2.Load<string>(GetInstanceID() + "Name");
            SpecValues = ES2.LoadDictionary<DB.SpecFactorType, long>(GetInstanceID() + "SpecValues");
        }

        [Title("Basic")]
        [OdinSerialize] public string Name { get; set; }

        [Title("Avator")]
        [OdinSerialize] public DB.Avator Avator { get; set; }

        [Title("Spec")]
        [OdinSerialize] public Dictionary<DB.SpecFactorType, long> SpecValues { get; set; } = new Dictionary<DB.SpecFactorType, long>();

        /// <summary>
        /// マップロード時に転送される場所です。
        /// </summary>
        [Title("Location")]
        [OdinSerialize, InfoBox("Please set transferred location when map loaded.")] public Location StartLocation { get; set; }

        /// <summary>
        /// 死亡時に復活する場所です。
        /// </summary>
        [OdinSerialize, InfoBox("Please set respawn location when died.")] public Location RessurectionLocation { get; set; }
    }
}