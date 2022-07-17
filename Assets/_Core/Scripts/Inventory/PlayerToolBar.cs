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
        public int SelectedSlot { get; private set; }

        private void Update()
        {
            if(UnityEngine.Input.GetKeyDown(KeyCode.Alpha1)) Select(0);
            if(UnityEngine.Input.GetKeyDown(KeyCode.Alpha2)) Select(1);
            if(UnityEngine.Input.GetKeyDown(KeyCode.Alpha3)) Select(2);
            if(UnityEngine.Input.GetKeyDown(KeyCode.Alpha4)) Select(3);
            if(UnityEngine.Input.GetKeyDown(KeyCode.Alpha5)) Select(4);
            if(UnityEngine.Input.GetKeyDown(KeyCode.Alpha6)) Select(5);
            if(UnityEngine.Input.GetKeyDown(KeyCode.Alpha7)) Select(6);
            if(UnityEngine.Input.GetKeyDown(KeyCode.Alpha8)) Select(7);
        }

        void Select(int index)
        {
            SelectedSlot = index;
            EventManager.OnItemSelected?.Invoke(Items[index] as WeaponItem);
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
        public bool MoveToInventory(int slotIndex)
        {
            if (Items[slotIndex] != null)
            {
                PlayerInventory.AddItem(Items[slotIndex]);
                Items[slotIndex] = null;
                return true;
            }

            return false;
        }
        public bool RemoveOnInventory(Item item)
        {
    return               PlayerInventory.RemoveItem(item);
        }

    }
}