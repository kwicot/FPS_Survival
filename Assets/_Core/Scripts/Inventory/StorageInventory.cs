using System.Collections.Generic;
using _Core.Scripts.Items;
using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 0162
namespace _Core.Scripts.InventorySystem
{
    
    public class StorageInventory : InventoryBase, IInteractable, IWeightBased
    {
        [SerializeField] protected float baseMaxWeight;

        public virtual float MaxWeight => baseMaxWeight;
        public virtual float Weight
        {
            get
            {
                var weight = 0f;
                foreach (var item in itemsList)
                    weight += item.TotalWeight;
                
                return weight;
            }
        }

        public float Overweight
        {
            get
            {
                if (Weight > MaxWeight)
                    return Weight / MaxWeight;

                return 1;
            }
        }

    }
}