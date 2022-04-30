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
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int tollBarSize;
        [SerializeField] private int rows;
        [SerializeField] private int columns;

        [SerializeField] private bool addStartItems;
        [SerializeField] private List<StartItem> startItems;

        private Item[,] items;


        public Item[,] Items => items;
        private void Start()
        {
            items = new Item[rows, columns];
            if (addStartItems)
            {
                foreach (var startItem in startItems)
                {
                    var item = startItem.item.Model;
                    item.Count += startItem.count;
                    AddItem(item);
                }
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

        public bool Move(Vector2Int from, Vector2Int to)
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
            }
            else if (items[toRow, toColumn].ID == items[fromRow, fromColumn].ID) //Add to exist item
            {
                
            }

            return true;
        }
        

    }
}