using System;
using _Core.Scripts.Items;
using UnityEngine;

namespace _Core.Scripts.InventorySystem.Models
{
    [Serializable]
    public class RandomItem
    {
        [SerializeField] private ItemSO itemSo;
        [SerializeField] private int minCount;
        [SerializeField] private int maxCount;
        [SerializeField] [Range(1,100)] private float change;

        public ItemSO ItemSo => itemSo;
        public int MinCount => minCount;
        public int MaxCount => maxCount;
        public float Change => change;
    }
}