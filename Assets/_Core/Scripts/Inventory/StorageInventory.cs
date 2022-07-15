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

        public override void AddItem(Item newItem)
        {
            //Debug.Log($"max weight {MaxWeight}. newItem weight {newItem.Weight}. newItem total weight {newItem.TotalWeight}");
            //Unstackable item
            if (newItem.CanStack == false)
            {
                    itemsList.Add(newItem);
                    //Debug.Log($"Add item {newItem.Name}");
                    OnStateChanged?.Invoke();
                    return ;
            }
            //Stackable item
            else
            {
                //Can add all items
                    //Add to exist item model
                    if (GetItem(newItem.ID, out var inventoryItem))
                        inventoryItem.Count += newItem.Count;
                    
                    //Add new item model
                    else
                        itemsList.Add(newItem);

                    //Debug.Log($"Add item {newItem.Name}");
                    OnStateChanged?.Invoke();
                    return;
            }
            
        }
    }
}