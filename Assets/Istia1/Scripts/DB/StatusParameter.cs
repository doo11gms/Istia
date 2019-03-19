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
    [System.Serializable]
    public class StatusParameter
    {
        [OdinSerialize] string m_ParameterName;
        public string ParameterName
        {
            get { return m_ParameterName; }
        }

        [OdinSerialize] int m_ValueOfInt;
        public int ValueOfInt
        {
            get { return m_ValueOfInt; }
        }

        [OdinSerialize] float m_ValueOfFloat;
        public float ValueOfFloat
        {
            get { return m_ValueOfFloat; }
        }

        //UINT,string

        public bool Increase<T>(T amount)
        {
            return true;
        }
    }
}