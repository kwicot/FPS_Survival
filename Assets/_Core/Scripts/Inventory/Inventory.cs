using System.Collections.Generic;
using _Core.Scripts.Items;
using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 0162
namespace _Core.Scripts.InventorySystem
{
    
    public class Inventory : MonoBehaviour, IInteractable
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

        private float FreeWeight => MaxWeight - Weight;
        
        

        protected List<Item> itemsList;

        public UnityAction OnStateChanged;

        public List<Item> Items
        {
            get
            {
                if (itemsList == null) return null;
                ClearNulls();
                return itemsList;
            }
        }

        void ClearNulls()
        {
            for (int i = itemsList.Count - 1; i >= 0; i--)
            {
                if(itemsList[i].Count <= 0)
                    itemsList.RemoveAt(i);
            }
        }

        private void Awake()
        {
            itemsList = new List<Item>();
        }

        public bool AddItem(Item newItem, out AddResult addResult)
        {
            Debug.Log($"max weight {MaxWeight}. Free weight {FreeWeight}. newItem weight {newItem.Weight}. newItem total weight {newItem.TotalWeight}");
            if (FreeWeight < newItem.Weight)
            {
                addResult = AddResult.Fail;
                Debug.Log($"Cant add {newItem.Name}");
                return false;
            }
            //Unstackable item
            if (newItem.CanStack == false)
            {
                if (FreeWeight >=  newItem.TotalWeight)
                {
                    itemsList.Add(newItem);
                    addResult = AddResult.All;
                    Debug.Log($"Add item {newItem.Name}");
                    OnStateChanged?.Invoke();
                    return true;
                }

                addResult = AddResult.Fail;
                Debug.Log($"Cant add item {newItem.Name}");
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
                    Debug.Log($"Add item {newItem.Name}");
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
                    Debug.Log($"Add item {newItem.Name}");
                    OnStateChanged?.Invoke();
                    return true;
                }
            }
            
        }

        // public bool AddItem(Item newItem, int targetIndex, out AddResult addResult)
        // {
        //     if (FreeWeight < newItem.Weight)
        //     {
        //         addResult = AddResult.Fail;
        //         return false;
        //     }
        //     
        //     //In last slot
        //     if (targetIndex >= itemsList.Count)
        //     {
        //         //Add all
        //         if (FreeWeight <= newItem.TotalWeight)
        //         {
        //             itemsList.Add(newItem);
        //             addResult = AddResult.All;
        //             return true;
        //         } 
        //     }
        // }


        public bool RemoveItem(Item item)
        {
            if (GetIndex(item, out int index))
            {
                itemsList.RemoveAt(index);
                Debug.Log($"Removed {item.Name}");
                return true;
            }

            return false;
        }

        public bool ContainsItem(Item targetItem)
        {
            foreach (var item in itemsList)
                if (targetItem == item)
                    return true;
            
            return false;
        }

        protected bool GetIndex(Item targetItem, out int index)
        {
            index = 0;
            if (!ContainsItem(targetItem)) return false;

            for (int i = 0; i < itemsList.Count; i++)
            {
                if (itemsList[i] == targetItem)
                {
                    index = i;
                    return true;
                }
            }

            return false;
        }

        bool GetItem(Item targetItem, out Item inventoryItem)
        {
            foreach (var item in itemsList)
            {
                if (item == targetItem)
                {
                    inventoryItem = item;
                    return true;
                }
            }

            inventoryItem = null;
            return false;
        }

        protected bool GetItem(string id, out Item item)
        {
            foreach (var inventoryItem in itemsList)
            {
                if (inventoryItem.ID == id)
                {
                    item = inventoryItem;
                    return true;
                }
            }

            item = null;
            return false;
        }

        // public MoveResult Move(Item targetItem, int toIndex)
        // {
        //     //Return fail if its new item and free space less than item weight
        //     bool isNew = ContainsItem(targetItem);
        //     if (FreeWeight < targetItem.Weight && !isNew) return MoveResult.Fail;
        //
        //     //New item
        //     if (isNew)
        //     {
        //         
        //     }
        //     //Exist item
        //     else
        //     {
        //         //Move to free place
        //         if (toIndex >= itemsList.Count)
        //         {
        //             
        //         }
        //     }
        //     
        //     //Move to free place
        //     if (toIndex >= itemsList.Count)
        //     {
        //         
        //     }
        //     //Move to exist item
        //
        //
        //     else if (targetItem.ID == items[toRow, toColumn].ID) //Add to exist item
        //     {
        //         if (GetIndex(targetItem, out var index))
        //         {
        //
        //         }
        //
        //         var itemFrom = targetItem;
        //         var itemTo = items[toRow, toColumn];
        //         if (itemFrom.Count < itemTo.CanAdd) //Add all
        //         {
        //             itemTo.Count += itemFrom.Count;
        //             if (GetIndex(itemFrom, out var indexFrom))
        //                 items[indexFrom.x, indexFrom.y] = null;
        //
        //             return MoveResult.MoveToExist;
        //         }
        //         else // Add part
        //         {
        //             var canAdd = itemTo.CanAdd;
        //             itemTo.Count += canAdd;
        //             itemFrom.Count -= canAdd;
        //
        //             return MoveResult.Part;
        //         }
        //
        //         OnStateChanged?.Invoke();
        //
        //     }
        //
        //     return MoveResult.Fail;
        // }
        

    }
}