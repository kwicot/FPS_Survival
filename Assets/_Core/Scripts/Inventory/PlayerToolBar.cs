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
        public StorageInventory PlayerInventory => playerController.Inventory;

        public Item[] Items { get; private set; } = new Item[8];

        public float Weight
        {
            get
            {
                float weight = 0;
                foreach (var item in Items)
                {
                    if (item != null)
                        weight += item.Weight;
                }

                return weight;
            }
        }
        
        


        public bool AddItem(Item item, int index)
        {
            if (Items[index] != null) return false;
            else
            {
                Items[index] = item;
                return true;
            }
        }

        public bool AddItem(Item item)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == null)
                {
                    Items[i] = item;
                    return true;
                }
            }

            return false;
        }

        public bool RemoveItem(Item item)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == item)
                {
                    Items[i] = null;
                    return true;
                }
            }

            return false;
        }
        public bool RemoveItem(int index)
        {
            if (Items[index] != null)
            {
                Items[index] = null;
                return true;
            }

            return false;
        }

    }
}