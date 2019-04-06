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
        [OdinSerialize, Required] PlayerStatus PlayerStatus { get; set; }

        [Title("Settings")]
        [OdinSerialize] DB.Parameter ItemLootingDistanceParameter { get; set; }

        List<Prop.Lootable> Lootables()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, PlayerStatus.CurrentParameterValues[ItemLootingDistanceParameter]);
            List<GameObject> hitObjects = hitColliders.Select(collider => collider.gameObject).ToList();
            List<Prop.Lootable> lootables = new List<Prop.Lootable>();
            foreach (var obj in hitObjects)
            {
                var component = obj.GetComponent<Prop.Lootable>();
                if (component != null) lootables.Add(component);
            }
            return lootables;
        }

        /// <summary>
        /// 近くのドロップアイテムを拾います。
        /// </summary>
        public void Execute()
        {
            var lootables = Lootables();
            if (lootables == null) return;
            if (lootables.Count == 0) return;

            Prop.Lootable nearest = null;
            float nearestDistance = Mathf.Infinity;
            Lootables().ForEach(item =>
            {
                var distance = Vector3.Distance(transform.position, item.transform.position);
                if (distance > nearestDistance) return;
                nearest = item;
                nearestDistance = distance;
            });

            for (int i = 0; i < nearest.Count; i++)
            {
                if (InventoryHandler.Push(nearest.InventoryItemInfo))
                {
                    nearest.Loot();
                }
                else
                {
                    Debug.Log("インベントリが一杯であるため、これ以上拾得できません。");
                    break;
                }
            }
            Destroy(nearest.gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(Config.KeyConfig.ItemLootingKey)) Execute();
        }
    }
}