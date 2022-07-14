using System.Collections.Generic;
using _Core.Scripts.Items;
using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 0162
namespace _Core.Scripts.InventorySystem
{
    
    public class PlayerInventory : InventoryBase, IInteractable, IWeightBased
    {
        [SerializeField] private float baseMaxWeight;

        public float MaxWeight => baseMaxWeight;

        public float Weight
        {
            get
            {
                var weight = 0f;
                foreach (var item in itemsList)
                    weight += item.TotalWeight;
                
                return weight;
            }
        }

        public float FreeWeight => MaxWeight - Weight;
        
        public override bool AddItem(Item newItem, out AddResult addResult)
        {
            //Debug.Log($"max weight {MaxWeight}. Free weight {FreeWeight}. newItem weight {newItem.Weight}. newItem total weight {newItem.TotalWeight}");
            if (FreeWeight < newItem.Weight)
            {
                addResult = AddResult.Fail;
                //Debug.Log($"Cant add {newItem.Name}");
                return false;
            }
            //Unstackable item
            if (newItem.CanStack == false)
            {
                if (FreeWeight >=  newItem.TotalWeight)
                {
                    itemsList.Add(newItem);
                    addResult = AddResult.All;
                    //Debug.Log($"Add item {newItem.Name}");
                    OnStateChanged?.Invoke();
                    return true;
                }

                addResult = AddResult.Fail;
                //Debug.Log($"Cant add item {newItem.Name}");
                return false;
            }
            //Stackable item
            else
            {
                //Can add all items
                if (newItem.TotalWeight <= FreeWeight)
                {
                    //Add to exist item model
                    if (GetItem(newItem.ID, out var inventoryItem))
                        inventoryItem.Count += newItem.Count;
                    
                    //Add new item model
                    else
                        itemsList.Add(newItem);

                    addResult = AddResult.All;
                    //Debug.Log($"Add item {newItem.Name}");
                    OnStateChanged?.Invoke();
                    return true;
                }
                //Can add part
                else
                {
                    float addWeight = 0f;
                    int canAdd = 0;
                    
                    for (int i = 0; i < newItem.Count; i++)
                    {
                        addWeight += newItem.Weight;
                        canAdd++;
                        
                        if((addWeight + newItem.Weight) > FreeWeight) break;
                    }

                    if (GetItem(newItem.ID, out var targetItem))
                    {
                        targetItem.Count += canAdd;
                    }
                    else
                    {
                        var model = newItem.Clone() as Item;
                        model.Count += canAdd;
                        itemsList.Add(model);
                    }
                    newItem.Count -= canAdd;
                    addResult = AddResult.Part;
                    //Debug.Log($"Add item {newItem.Name}");
                    OnStateChanged?.Invoke();
                    return true;
                }
            }
            
        }
    }
}