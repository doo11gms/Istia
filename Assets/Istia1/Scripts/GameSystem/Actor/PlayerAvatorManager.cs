using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.GameSystem.Actor
{
    public class PlayerAvatorManager : SerializedMonoBehaviour
    {
	    [OdinSerialize, Required] Profile.PlayerProfile PlayerProfile { get; set; }

        GameObject m_CurrentModel;
        List<GameObject> m_InstantiatedModels;

        private void Awake()
        {
            m_InstantiatedModels = new List<GameObject>();
        }

        private void Update()
        {
            var nextModel = PlayerProfile.Avator.Model;

            if (m_CurrentModel != nextModel)
            {
                m_InstantiatedModels.ForEach(model => Destroy(model));
                m_InstantiatedModels.Clear();
                m_InstantiatedModels.Add(Instantiate(nextModel, transform));
                m_CurrentModel = nextModel;
            }
        }
    }
}