using System;
using System.Collections.Generic;
using System.Linq;
using _Core.Scripts.Items;
using _Core.Scripts.Player;
using UnityEngine;

namespace _Core.Scripts.InventorySystem
{
    public class PlayerToolBar : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        private StorageInventory playerInventory => playerController.Inventory;

        private Item[] items;

        public float Weight
        {
            get
            {
                float weight = 0;
                foreach (var item in items)
                {
                    if (item != null)
                        weight += item.Weight;
                }

                return weight;
            }
        }
        

        private void Start()
        {
            items = new Item[8];
        }

        public bool AddItem(Item item, int index)
        {
            if (items[index] != null) return false;
            else
            {
                items[index] = item;
                return true;
            }
        }

        public bool AddItem(Item item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    items[i] = item;
                    return true;
                }
            }

            return false;
        }

        public bool RemoveItem(Item item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == item)
                {
                    items[i] = null;
                    return true;
                }
            }

            return false;
        }
        public bool RemoveItem(int index)
        {
            if (items[index] != null)
            {
                items[index] = null;
                return true;
            }

            return false;
        }

    }
}