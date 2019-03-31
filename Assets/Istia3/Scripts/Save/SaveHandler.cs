using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia3.Save
{
    [CreateAssetMenu(fileName = "SaveHandler", menuName = "Istia3/Save/SaveHandler")]
    public class SaveHandler : SerializedScriptableObject
    {
	    public static string Path(Object obj, string name)
        {
            return obj.GetInstanceID() + name;
        }

        public static void Save<T>(Object obj, T target, string name)
        {
            var path = Path(obj, name);
            if (target.GetType() == typeof(int)) PlayerPrefs.SetInt(path, (int)((object)target));
            if (target.GetType() == typeof(string)) PlayerPrefs.SetString(path, (string)((object)(target)));
            PlayerPrefs.Save();
        }

        public static void Load<T>(Object obj, ref T target, string name)
        {
            var path = Path(obj, name);
            if (target.GetType() == typeof(int)) target = (T)(object)(PlayerPrefs.GetInt(path));
            if (target.GetType() == typeof(string)) target = (T)(object)(PlayerPrefs.GetString(path));
        }

        [Button("Delete")]
        public static void Delete()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}