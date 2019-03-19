using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.Extension
{
    public static class Vector3Extension
    {
        public static Vector3 Flatten(this Vector3 self) => new Vector3(self.x, 0f, self.z);
        public static Vector3 RandomDirection(this Vector3 self) =>
            new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }
}