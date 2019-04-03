using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.GameSystem.Actor.Player
{
    public class PlayerStatus : StatusBase, Save.ISavable
    {
        void Save.ISavable.Save()
        {
        }

        void Save.ISavable.Load()
        {
        }

        /// <summary>
        /// マップロード時に転送される場所です。
        /// </summary>
        [Title("Location")]
        [OdinSerialize, InfoBox("Please set transferred location when map loaded.")] public Meta.Location StartLocation { get; set; }

        /// <summary>
        /// 死亡時に復活する場所です。
        /// </summary>
        [OdinSerialize, InfoBox("Please set respawn location when died.")] public Meta.Location RessurectionLocation { get; set; }

        [TitleGroup("Required")]
        [OdinSerialize, Required] EquipHandler EquipHandler;

        void StatusCorrectByEquipments()
        {
            foreach (var equipment in EquipHandler.Equipments())
            {
                foreach (var key in equipment.ParameterValues.Keys)
                {
                    if (!CurrentParameterValues.ContainsKey(key)) throw new System.Exception("対象のパラメータが見つかりません。");
                    CurrentParameterValues[key] += equipment.ParameterValues[key];
                }
            }
        }

        protected override void Update()
        {
            base.Update();
            StatusCorrectByEquipments();
        }
    }
}