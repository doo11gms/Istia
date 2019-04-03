using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.GameSystem.Item
{
    public class DropItemLooter : SerializedMonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(Config.KeyConfig.DropItemLootingKey))
            {
                Collider[] hits = Physics.OverlapSphere(transform.position, 5f);

                foreach(var hit in hits)
                {
                    Debug.Log(hit.name);
                }
            }
        }
    }
}