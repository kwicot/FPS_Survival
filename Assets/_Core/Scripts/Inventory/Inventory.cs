using System;
using System.Collections.Generic;
using _Core.Scripts;
using _Core.Scripts.Items;
using UnityEngine;
using UnityEngine.Events;

namespace Player.Core
{
    [Serializable]
    class StartItem
    {
        public ItemSO item;
        public int count;
    }

    public enum MoveResult
    {
        MoveToEmpty,
        MoveToExist,
        Fail,
        Part
    }
    public class Inventory : MonoBehaviour, IInteractable
    {
        [SerializeField] private int rows;
        [SerializeField] private int columns;

        [SerializeField] private bool addStartItems;
        [SerializeField] private List<StartItem> startItems;

        private Item[,] items;

        public UnityAction OnStateChanged;
        public Item[,] Items
        {
            get
            {
                if (items == null) return null;
                
                for (int row = 0; row < rows; row++)
                {
                    for (int column = 0; column < columns; column++)
                    {
                        if (items[row,column] != null && items[row, column].Count <= 0)
                            items[row, column] = null;
                    }
                }

                return items;
            }
        }

        private void Awake()
        {
            items = new Item[rows, columns];
            if (addStartItems)
                AddStartItems();
        }

        void AddStartItems()
        {
            foreach (var startItem in startItems)
            {
                var item = startItem.item.Model;
                item.Count = startItem.count;
                AddItem(item);
            }
        }

        public bool AddItem(Item item)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (items[row, column] == null)
                    {
                        items[row, column] = item;
                        OnStateChanged?.Invoke();
                        Debug.Log($"Added {item.Name} {item.Count}");
                        return true;
                    }
                    
                }
            }

            return false;
        }

        public bool RemoveItem(Item item)
        {
            if (GetIndex(item, out var index))
            {
                items[index.x, index.y] = null;
                Debug.Log($"Removed {item.Name}");
                return true;
            }

            return false;
        }

        public bool ContainsItem(Item item)
        {
            foreach (var item1 in items)
                if (item == item1)
                    return true;
            
            return false;
        }

        bool GetIndex(Item item, out Vector2Int index)
        {
            index = Vector2Int.zero;
            if (!ContainsItem(item)) return false;
            
            for (int row = 0; row < rows; row++)
            {
                for (int columnn = 0; columnn < columns; columnn++)
                {
                    if (items[row, columnn] == item)
                    {
                        index = new Vector2Int(row, columnn);
                        return true;
                    }
                }
            }

            return false;
        }

        public MoveResult Move(Item targetItem, Vector2Int toIndex)
        {
            
            var toRow = toIndex.x;
            var toColumn = toIndex.y;

            if (items[toRow, toColumn] == null ) //Move to free place
            {
                if(GetIndex(targetItem,out var index))
                    items[index.x, index.y] = null;

                items[toRow, toColumn] = targetItem;
                OnStateChanged?.Invoke();
                return MoveResult.MoveToEmpty;
            }
            else if (targetItem.ID == items[toRow, toColumn].ID) //Add to exist item
            {
                if (GetIndex(targetItem, out var index))
                {

                }

                var itemFrom = targetItem;
                var itemTo = items[toRow, toColumn];
                if (itemFrom.Count < itemTo.CanAdd) //Add all
                {
                    itemTo.Count += itemFrom.Count;
                    if (GetIndex(itemFrom, out var indexFrom))
                        items[indexFrom.x, indexFrom.y] = null;

                    return MoveResult.MoveToExist;
                }
                else // Add part
                {
                    var canAdd = itemTo.CanAdd;
                    itemTo.Count += canAdd;
                    itemFrom.Count -= canAdd;

                    return MoveResult.Part;
                }

                OnStateChanged?.Invoke();

            }

            return MoveResult.Fail;
        }
        

    }
}