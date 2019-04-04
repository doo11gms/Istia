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
    public class SaveHandler : SerializedMonoBehaviour
    {
        [OdinSerialize] List<ISavable> ISavables { get; set; } = new List<ISavable>();

        static int IntOfBool(bool value) => value ? 1 : 0;
        static bool BoolOfInt(int value) => value == 1;

	    public static string Path(Object instance, string name)
        {
            return instance.GetInstanceID() + name;
        }

        public static void Save<T>(Object instance, T target, string name)
        {
            var path = Path(instance, name);
            if (target == null)
            {
                PlayerPrefs.SetString(path, null);
                return;
            }
            if (target.GetType() == typeof(int)) PlayerPrefs.SetInt(path, (int)((object)target));
            if (target.GetType() == typeof(string)) PlayerPrefs.SetString(path, (string)((object)(target)));
            if (target.GetType() == typeof(bool)) PlayerPrefs.SetInt(path, IntOfBool((bool)((object)target)));
            PlayerPrefs.Save();
        }

        public static void Load<T>(Object instance, ref T target, string name)
        {
            target = Load<T>(instance, name);
        }

        public static T Load<T>(Object instance, string id)
        {
            var path = Path(instance, id);
            if (!PlayerPrefs.HasKey(path)) return default;
            if (id == null) return (T)(object)(PlayerPrefs.GetString(path));
            if (typeof(T) == typeof(int)) return (T)(object)(PlayerPrefs.GetInt(path));
            if (typeof(T) == typeof(string)) return (T)(object)(PlayerPrefs.GetString(path));
            if (typeof(T) == typeof(bool)) return (T)(object)(BoolOfInt(PlayerPrefs.GetInt(path)));
            return default;
        }

        [Button("Save")]
        public void Save()
        {
            if (ISavables == null) return;
            ISavables.ForEach(savable => savable.Save());
            Debug.Log("Saved.");
        }

        [Button("Load")]
        public void Load()
        {
            if (ISavables == null) return;
            ISavables.ForEach(savable => savable.Load());
            Debug.Log("Loaded.");
        }
        
        [Button("Delete")]
        public static void Delete()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Deleted.");
        }
    }
}