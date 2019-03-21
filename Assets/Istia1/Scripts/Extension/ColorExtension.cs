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
    public static class ColorExtension
    {
        public static Color SetAlpha(this Color self, float alpha) => new Color(self.r, self.g, self.b, alpha);
    }
}