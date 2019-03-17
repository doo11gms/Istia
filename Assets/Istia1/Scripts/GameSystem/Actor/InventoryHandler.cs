using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia1.GameSystem.Actor
{
    public class InventoryHandler : SerializedMonoBehaviour
    {
        [Title("Required")]
        [OdinSerialize, Required] Profile.InventoryProfile m_InventoryProfile;

        public void Initialize()
        {

        }

	    public void Push()
        {

        }

        public void Pop()
        {

        }

        public void Dispose()
        {

        }

        public void DisposeAll()
        {

        }

        /// <summary>
        /// プロファイルの内容をUIに反映します。
        /// </summary>
        public void ApplyProfile()
        {
            
        }
    }
}