using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia2.DB
{
    [CreateAssetMenu(fileName = "Parameter", menuName = "Istia2/DB/Parameter")]
    public class Parameter : SerializedScriptableObject, Save.ISavable
    {
        void Save.ISavable.Save()
        {
            ES2.Save(Name, GetInstanceID() + "Name");
            ES2.Save(Abbreviation, GetInstanceID() + "Abbreviation");
            ES2.Save(UsingPercentage, GetInstanceID() + "UsingPercentage");
        }

        void Save.ISavable.Load()
        {
            Name = ES2.Load<string>("Name");
            Abbreviation = ES2.Load<string>("Abbreviation");
            UsingPercentage = ES2.Load<bool>("UsingPercentage");
        }

        [Title("Basic")]
        [OdinSerialize] public string Name { get; set; }
        [OdinSerialize] public string Abbreviation { get; set; }

        [Title("Advanced")]
        [OdinSerialize] public bool UsingPercentage { get; set; } = false;
    }
}