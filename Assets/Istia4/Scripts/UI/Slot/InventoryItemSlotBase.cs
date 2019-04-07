using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace EllGames.Istia4.UI.Slot
{
    public abstract class InventorySlotBase : SlotBase
    {
        public abstract bool IsEmpty();
        public abstract void Emptimize();
        public abstract void Dispose();
        public abstract void DisposeAll();
        public abstract void Drop();
        public abstract void DropAll();
        public abstract bool Push(DB.Inventory.InventoryItemInfoBase inventoryInfoBase);
        public abstract void Refresh();
    }
}