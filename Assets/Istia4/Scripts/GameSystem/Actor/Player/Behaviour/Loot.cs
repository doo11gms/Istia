using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Linq;

namespace EllGames.Istia4.GameSystem.Actor.Player.Behaviour
{
    public class Loot : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] InventoryHandler InventoryHandler { get; set; }

        [Title("Settings")]
        [OdinSerialize] float LootingDistance { get; set; } = 5f;

        /// <summary>
        /// 近くのドロップアイテムを拾います。
        /// </summary>
        public void Execute()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, LootingDistance);
            if (hitColliders.Count() == 0) return;

            List<GameObject> hitObjects = hitColliders.Select(collider => collider.gameObject).ToList();
            List<Prop.DropItem> dropItems = new List<Prop.DropItem>();
            foreach (var obj in hitObjects)
            {
                var component = obj.GetComponent<Prop.DropItem>();
                if (component != null) dropItems.Add(component);
            }

            Prop.DropItem nearest = null;
            float nearestDistance = Mathf.Infinity;
            dropItems.ForEach(item =>
            {
                var distance = Vector3.Distance(transform.position, item.transform.position);
                if (distance > nearestDistance) return;
                nearest = item;
                nearestDistance = distance;
            });

            InventoryHandler.Push(nearest.InventoryItemInfo);
            Destroy(nearest.gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(Config.KeyConfig.ItemLootingKey)) Execute();
        }
    }
}