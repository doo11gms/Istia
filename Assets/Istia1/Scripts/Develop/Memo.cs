using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.Develop
{
    public class Memo : SerializedMonoBehaviour
    {
        [OdinSerialize, TextArea(20, 20)] string m_Text;
    }
}