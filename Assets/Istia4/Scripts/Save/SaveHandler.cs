using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.Save
{
    [CreateAssetMenu(fileName = "SaveHandler", menuName = "Istia4/Save/SaveHandler")]
    public class SaveHandler : SerializedScriptableObject
    {
        static int IntOfBool(bool value) => value ? 1 : 0;
        static bool BoolOfInt(int value) => value == 1;

	    public static string Path(Object obj, string name)
        {
            return obj.GetInstanceID() + name;
        }

        public static void Save<T>(Object obj, T target, string name)
        {
            if (target == null) return;
            var path = Path(obj, name);
            if (target.GetType() == typeof(int)) PlayerPrefs.SetInt(path, (int)((object)target));
            if (target.GetType() == typeof(string)) PlayerPrefs.SetString(path, (string)((object)(target)));
            if (target.GetType() == typeof(bool)) PlayerPrefs.SetInt(path, IntOfBool((bool)((object)target)));
            PlayerPrefs.Save();
        }

        public static void Load<T>(Object obj, ref T target, string name)
        {
            var path = Path(obj, name);
            if (!PlayerPrefs.HasKey(path)) return;
            if (target == null)
            {
                target = (T)(object)(PlayerPrefs.GetString(path));
                return;
            }
            if (target.GetType() == typeof(int)) target = (T)(object)(PlayerPrefs.GetInt(path));
            if (target.GetType() == typeof(string)) target = (T)(object)(PlayerPrefs.GetString(path));
            if (target.GetType() == typeof(bool)) target = (T)(object)(BoolOfInt(PlayerPrefs.GetInt(path)));
        }

        [Button("Delete")]
        public static void Delete()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}