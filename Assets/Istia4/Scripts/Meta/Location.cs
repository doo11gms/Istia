using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.Meta
{
    [System.Serializable]
    public struct Location
    {
        public string scene;
        public Vector3 position;
        public Vector3 eulerAngles;

        public Location(string scene, Vector3 position, Vector3 eulerAngles)
        {
            this.scene = scene;
            this.position = position;
            this.eulerAngles = eulerAngles;
        }
    }
}