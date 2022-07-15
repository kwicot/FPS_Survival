using System.Collections.Generic;
using _Core.Scripts.Items;
using UnityEngine;
using UnityEngine.Events;

namespace _Core.Scripts.InventorySystem
{
    public class InventoryBase : MonoBehaviour, IInteractable
    {
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
        
        private void Awake()
        {
            itemsList = new List<Item>();
        }
        
        public virtual bool AddItem(Item newItem, out AddResult addResult)
        {
            if (newItem.CanStack == false)
            {
                    itemsList.Add(newItem);
                    addResult = AddResult.All;
                    //Debug.Log($"Add item {newItem.Name}");
                    OnStateChanged?.Invoke();
                    return true;
            }
            else
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
            
        }
        
        public bool RemoveItem(Item item)
        {
            if (GetIndex(item, out int index))
            {
                itemsList.RemoveAt(index);
                //Debug.Log($"Removed {item.Name}");
                OnStateChanged?.Invoke();
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
        
        protected bool GetItem(Item targetItem, out Item inventoryItem)
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
        
        
        void ClearNulls()
        {
            for (int i = itemsList.Count - 1; i >= 0; i--)
            {
                if(itemsList[i].Count <= 0)
                    itemsList.RemoveAt(i);
            }
        }
    }
}