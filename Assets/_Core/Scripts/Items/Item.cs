using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Core.Scripts.Items
{
    [Serializable]
    public class Item
    {
        [SerializeField] protected string id;
        [SerializeField] protected string name;
        [SerializeField] protected float basePrice;
        [SerializeField] protected bool canStack;
        [SerializeField] protected Sprite image;
        [SerializeField] protected float weight;

        protected int count;
        
        public string ID => id;
        public string Name => name;
        public float BasePrice => basePrice;
        public bool CanStack => canStack;
        public float Weight => weight;

        public float TotalWeight
        {
            get
            {
                if (canStack == false) return weight;
                return weight * count;
            }
        }

        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnCountChanged?.Invoke(count);
            }
        }
        public Sprite Image => image;

        public UnityAction<int> OnCountChanged;

        public virtual object Clone(bool copyCount = false) => new Item(id, name, basePrice,canStack, weight, image, copyCount ? count : 1);

        public Item(string id, string name, float basePrice,bool canStack,float weight, Sprite image, int count)
        {
            this.count = count;
            this.id = id;
            this.name = name;
            this.basePrice = basePrice;
            this.canStack = canStack;
            this.weight = weight;
            this.image = image;
        }
    }
}