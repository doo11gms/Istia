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
            m_StartLocation = new Meta.Location(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, transform.position, transform.eulerAngles);

            Save.SaveHandler.Save(this, m_StartLocation.scene,　nameof(m_StartLocation) + nameof(m_StartLocation.scene));
            Save.SaveHandler.Save(this, m_StartLocation.position, nameof(m_StartLocation) + nameof(m_StartLocation.position));
            Save.SaveHandler.Save(this, m_StartLocation.eulerAngles, nameof(m_StartLocation) + nameof(m_StartLocation.eulerAngles));

            Save.SaveHandler.Save(this, m_RessurectionLocation.scene, nameof(m_RessurectionLocation) + nameof(m_RessurectionLocation.scene));
            Save.SaveHandler.Save(this, m_RessurectionLocation.position, nameof(m_RessurectionLocation) + nameof(m_RessurectionLocation.position));
            Save.SaveHandler.Save(this, m_RessurectionLocation.eulerAngles, nameof(m_RessurectionLocation) + nameof(m_RessurectionLocation.eulerAngles));
        }

        void Save.ISavable.Load()
        {
            var startLocation = new Meta.Location();
            Save.SaveHandler.Load(this, ref startLocation.scene, nameof(m_StartLocation) + nameof(m_StartLocation.scene));
            Save.SaveHandler.Load(this, ref startLocation.position, nameof(m_StartLocation) + nameof(m_StartLocation.position));
            Save.SaveHandler.Load(this, ref startLocation.eulerAngles, nameof(m_StartLocation) + nameof(m_StartLocation.eulerAngles));
            m_StartLocation = startLocation;

            var ressurectionLocation = new Meta.Location();
            Save.SaveHandler.Load(this, ref ressurectionLocation.scene, nameof(m_RessurectionLocation) + nameof(m_RessurectionLocation.scene));
            Save.SaveHandler.Load(this, ref ressurectionLocation.position, nameof(m_RessurectionLocation) + nameof(m_RessurectionLocation.position));
            Save.SaveHandler.Load(this, ref ressurectionLocation.eulerAngles, nameof(m_RessurectionLocation) + nameof(m_RessurectionLocation.eulerAngles));
            m_RessurectionLocation = ressurectionLocation;
        }

        /// <summary>
        /// マップロード時に転送される場所です。
        /// </summary>
        [Title("Location")]
        [OdinSerialize, InfoBox("Please set transferred location when map loaded.")] Meta.Location m_StartLocation;
        public Meta.Location StartLocation
        {
            get { return m_StartLocation; }
            set { m_StartLocation = value; }
        }

        /// <summary>
        /// 死亡時に復活する場所です。
        /// </summary>
        [OdinSerialize, InfoBox("Please set respawn location when died.")] Meta.Location m_RessurectionLocation;
        public Meta.Location RessurectionLocation
        {
            get { return m_RessurectionLocation; }
            set { m_RessurectionLocation = value; }
        }

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