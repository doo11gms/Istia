using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.GameSystem.Shortcut
{
    [CreateAssetMenu(fileName = "ShortcutInfoContainerFactory", menuName = "Istia4/Factory/ShortcutInfoContainerFactory")]
    public class ShortcutInfoContainerFactory : SerializedScriptableObject
    {
	    public GameObject New(ShortcutInfo shortcutInfo)
        {
            var canvas = FindObjectOfType<Canvas>();
            if (canvas == null) throw new System.Exception("Canvasが見つかりません。");

            var obj = new GameObject();
            obj.transform.parent = canvas.transform;
            obj.AddComponent<Image>();
            obj.GetComponent<Image>().sprite = shortcutInfo.IconSprite;
            obj.AddComponent<UI.Container.ShortcutInfoContainer>();
            obj.GetComponent<UI.Container.ShortcutInfoContainer>().Assign(shortcutInfo);

            return obj;
        }
    }
}