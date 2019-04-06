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
    public class TransformSavable : SerializedMonoBehaviour, ISavable
    {
	    void ISavable.Save()
        {
            SaveHandler.Save(this, transform.position, nameof(transform.position));
            SaveHandler.Save(this, transform.eulerAngles, nameof(transform.eulerAngles));
            SaveHandler.Save(this, transform.localScale, nameof(transform.localScale));
        }

        void ISavable.Load()
        {
            transform.position = SaveHandler.Load<Vector3>(this, nameof(transform.position));
            transform.eulerAngles = SaveHandler.Load<Vector3>(this, nameof(transform.eulerAngles));
            transform.localScale = SaveHandler.Load<Vector3>(this, nameof(transform.localScale));
        }
    }
}