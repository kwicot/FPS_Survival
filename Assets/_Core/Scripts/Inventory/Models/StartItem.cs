using System;
using _Core.Scripts.Items;
using UnityEngine;

namespace _Core.Scripts.InventorySystem.Models
{
    [Serializable]
    public class StartItem<T> where T : ScriptableObject
    {
        public T itemSO;
        public int count;
    }
}