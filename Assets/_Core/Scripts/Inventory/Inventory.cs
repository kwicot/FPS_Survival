using System;
using System.Collections.Generic;
using _Core.Scripts.Items;
using _Core.Scripts.UI;
using UnityEngine;

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
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int tollBarSize;
        [SerializeField] private int rows;
        [SerializeField] private int columns;

        [SerializeField] private bool addStartItems;
        [SerializeField] private List<StartItem> startItems;

        private Item[,] items;


        public Item[,] Items => items;

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
                        return true;
                    }
                    
                }
            }

            return false;
        }

        public MoveResult Move(Vector2Int from, Vector2Int to)
        {
            var fromRow = from.x;
            var fromColumn = from.y;

            var toRow = to.x;
            var toColumn = to.y;

            if (items[toRow, toColumn] == null ) //Move to free place
            {
                var item = Items[fromRow, fromColumn];
                items[fromRow, fromColumn] = null;
                items[toRow, toColumn] = item;
                return MoveResult.MoveToEmpty;
            }
            else if (items[toRow, toColumn].ID == items[fromRow, fromColumn].ID) //Add to exist item
            {
                var itemFrom = items[fromRow, fromColumn];
                var itemTo = items[toRow, toColumn];
                if (itemFrom.Count < itemTo.CanAdd) //Add all
                {
                    itemTo.Count += itemFrom.Count;
                    items[fromRow, fromColumn] = null;
                    return MoveResult.MoveToExist;
                }
                else // Add part
                {
                    var canAdd = itemTo.CanAdd;
                    itemTo.Count += canAdd;
                    itemFrom.Count -= canAdd;
                    return MoveResult.Part;
                }

            }

            return MoveResult.Fail;
        }
        

    }
}