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
    [CreateAssetMenu(menuName = "Istia4/Provider/ParameterProvider", fileName = "ParameterProvider")]
    public class ParameterProvider : SerializedScriptableObject
    {
        [OdinSerialize] public List<Parameter> Providables { get; set; } = new List<Parameter>();

        public Parameter Provide(string id)
        {
            foreach (var parameter in Providables)
            {
                if (parameter.ID == id) return parameter;
            }

            throw new System.Exception("IDに対応するパラメータが見つかりません。");
        }
    }
}
