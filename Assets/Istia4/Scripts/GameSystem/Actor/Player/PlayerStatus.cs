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
        #region Save and Load

        void Save.ISavable.Save()
        {
            Save.SaveHandler.Save(this, m_Name, nameof(m_Name));
            Save.SaveHandler.Save(this, m_AvatorID, nameof(m_AvatorID));

            ES2.Save(m_AccExp, GetInstanceID() + nameof(m_AccExp));

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
            Save.SaveHandler.Load(this, ref m_Name, nameof(m_Name));
            Save.SaveHandler.Load(this, ref m_AvatorID, nameof(m_AvatorID));

            m_AccExp = ES2.Load<long>(GetInstanceID() + nameof(m_AccExp));

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

        #endregion

        [TitleGroup("Basic")]
        [OdinSerialize] string m_Name;
        public string Name
        {
            get { return m_Name; }
        }

        [Title("Avator")]
        [OdinSerialize] string m_AvatorID;
        public string AvatorID
        {
            get { return m_AvatorID; }
        }

        [TitleGroup("Required")]
        [OdinSerialize, Required, PropertyOrder(0)] EquipHandler EquipHandler { get; set; }

        [TitleGroup("Required"), PropertyOrder(0)]
        [OdinSerialize, Required] DB.ExpTableProvider ExpTableProvider { get; set; }

        [TitleGroup("Level"), PropertyOrder(10) ]
        [OdinSerialize, InfoBox("This value is automatically corrected depending on AccExp.")] DB.Parameter LevelParameter { get; set; }

        [TitleGroup("Level"), PropertyOrder(11)]
        [OdinSerialize] long m_AccExp;
        public long AccExp
        {
            get { return m_AccExp; }
        }

        /// <summary>
        /// マップロード時に転送される場所です。
        /// </summary>
        [Title("Location")]
        [OdinSerialize, InfoBox("Please set transferred location when map loaded."), PropertyOrder(12)] Meta.Location m_StartLocation;
        public Meta.Location StartLocation
        {
            get { return m_StartLocation; }
            set { m_StartLocation = value; }
        }

        /// <summary>
        /// 死亡時に復活する場所です。
        /// </summary>
        [OdinSerialize, InfoBox("Please set respawn location when died."), PropertyOrder(13)] Meta.Location m_RessurectionLocation;
        public Meta.Location RessurectionLocation
        {
            get { return m_RessurectionLocation; }
            set { m_RessurectionLocation = value; }
        }

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

        void StatusCorrectByExp()
        {
            int currentLevel = 1;
            for (int level = 1; level < Config.GameConfig.LevelCap; level++)
            {
                if (!ExpTableProvider.Provide().ContainsKey(level)) throw new System.Exception("レベルに対応する必要経験値がEXPテーブルに存在しません。");
                if (m_AccExp >= ExpTableProvider.Provide()[level]) currentLevel = level;
                else break;
            }

            if (!CurrentParameterValues.ContainsKey(LevelParameter)) throw new System.Exception("パラメータが見つかりません。");
            CurrentParameterValues[LevelParameter] = currentLevel;
        }

        protected override void Update()
        {
            base.Update();
            StatusCorrectByExp();
            StatusCorrectByEquipments();
        }

        [Title("Buttons")]
        [Button("Add Exp"), PropertyOrder(101)]
        public void AddExp(long exp)
        {
            m_AccExp += exp;
        }
    }
}