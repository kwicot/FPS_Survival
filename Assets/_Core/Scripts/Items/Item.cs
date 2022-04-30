using System;
using UnityEngine;

namespace _Core.Scripts.Items
{
    [Serializable]
    public class Item : ICloneable
    {
        [SerializeField] protected string id;
        [SerializeField] protected string name;
        [SerializeField] protected float basePrice;
        [SerializeField] protected int maxCount;
        [SerializeField] protected Sprite image;
        
        [SerializeField] protected ItemCategory category;

        protected int count;
        
        public string ID => id;
        public string Name => name;
        public float BasePrice => basePrice;
        public int MaxCount => maxCount;

        public int Count
        {
            get => count;
            set => count = value;
        }
        public Sprite Image => image;
        
        public object Clone() => new Item(id, name, basePrice,maxCount, image, category);

        public Item(string id, string name, float basePrice,int maxCount, Sprite image, ItemCategory category)
        {
            count = 1;
            this.id = id;
            this.name = name;
            this.basePrice = basePrice;
            this.maxCount = maxCount;
            this.image = image;
            this.category = category;
        }
    }
}