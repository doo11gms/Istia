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
    [CreateAssetMenu(menuName = "Istia1/DB/Status", fileName = "Status")]
    [System.Serializable]
    public class Status : SerializedScriptableObject
    {
        [OdinSerialize] List<StatusParameter> StatusParameters;

        [Title("Level")]
        [OdinSerialize] public int Level { get; set; } = 1;
        [OdinSerialize] public int MaxLevel { get; set; } = 99;
        [OdinSerialize] public uint Exp { get; set; }
        [OdinSerialize] public uint NextExp { get; set; } = 9999;

        [Title("Battle")]
        [OdinSerialize] public int MaxHP { get; set; } = 1000;
        [OdinSerialize] public int HP { get; set; } = 1000;
        [OdinSerialize] public int MaxMP { get; set; } = 1000;
        [OdinSerialize] public int MP { get; set; } = 1000;
        [OdinSerialize] public int MaxAtk { get; set; } = 10;
        [OdinSerialize] public int Atk { get; set; } = 10;
        [OdinSerialize] public int MaxDef { get; set; } = 10;
        [OdinSerialize] public int Def { get; set; } = 10;
        [OdinSerialize] public int MaxMat { get; set; } = 10;
        [OdinSerialize] public int Mat { get; set; } = 10;
        [OdinSerialize] public int MaxMdf { get; set; } = 10;
        [OdinSerialize] public int Mdf { get; set; } = 10;

        [Title("Move")]
        [OdinSerialize] public float WalkSpeed { get; set; } = 1.5f;
        [OdinSerialize] public float RunSpeed { get; set; } = 3f;

        [Title("Advanced")]
        /// <summary>
        /// クリティカル発生率
        /// </summary>
        [OdinSerialize, LabelText("Critical Rate")] public float CR { get; set; } = 0.05f;

        /// <summary>
        /// クリティカルダメージ倍率
        /// </summary>
        [OdinSerialize, LabelText("Critical Damage Mag")] public float Cdm { get; set; } = 1.5f;

        /// <summary>
        /// マネードロップ率
        /// </summary>
        [OdinSerialize, LabelText("Money Drop Rate")] public float Mdr { get; set; } = 0.1f;

        /// <summary>
        /// マネードロップ倍率
        /// </summary>
        [OdinSerialize, LabelText("Money Drop Mag")] public float Mdm { get; set; } = 1f;

        /// <summary>
        /// アイテムドロップ率
        /// </summary>
        [OdinSerialize, LabelText("Item Drop Rate")] public float Idr { get; set; } = 0.05f;

        /// <summary>
        /// 経験値倍率
        /// </summary>
        [OdinSerialize, LabelText("Exp Mag")] public float EM { get; set; } = 1f;

        void Copy(Status status)
        {
            Level = status.Level;
            Debug.Log("TODO");
        }

        [Title("Buttons")]

        [Button("Initialize")]
        public void Initialize()
        {
            Copy(new Status());
        }
    }
}