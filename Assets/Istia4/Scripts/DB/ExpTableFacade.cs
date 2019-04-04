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
    [CreateAssetMenu(menuName = "Istia4/Facade/ExpTableFacade", fileName = "ExpTableFacade")]
    public class ExpTableFacade : SerializedScriptableObject
    {
        [Title("Required")]
        [OdinSerialize, Required] ExpTableProvider ExpTableProvider { get; set; }
        [OdinSerialize, Required] ExpTableFactory ExpTableFactory { get; set; }

        [Title("Settings")]
        [OdinSerialize] int MaxLevel { get; set; } = 10;

        [Button("Update ExpTable")]
        public void UpdateExpTable()
        {
            if (MaxLevel > 500) throw new System.Exception("許可されていない値であるため、実行できませんでした。");
            if (ExpTableFactory == null) throw new System.Exception();
            if (ExpTableFactory == null) throw new System.Exception();
            ExpTableProvider.ExpTable = ExpTableFactory.CreateExpTable(MaxLevel);
        }

        [Button("Clear ExpTable")]
        public void ClearExpTable()
        {
            ExpTableProvider.ExpTable = new Dictionary<int, long>();
        }
    }
}
