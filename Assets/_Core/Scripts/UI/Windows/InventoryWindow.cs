using System.Collections.Generic;
using _Core.Scripts.InventorySystem;
using _Core.Scripts.Items;
using _Core.Scripts.UI.MainMenu;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0472

namespace _Core.Scripts.UI
{
    public class InventoryWindow : WindowBase
    {
        [SerializeField] private InventoryBase targetInventory;
        
        [SerializeField] protected GameObject slotPrefab;
        [SerializeField] protected GameObject itemPrefab;
        [SerializeField] protected Transform cellsParent;

        [SerializeField] private Text currentWeightText;
        [SerializeField] private Text maxWeightText;

        [SerializeField] private ItemInfoPanel infoPanel;

        private List<ItemSlot> slots = new List<ItemSlot>();


        private void Start()
        {
            
        }

        public void SetInventory(InventoryBase target)
        {
            targetInventory = target;
            Debug.Log($"Set inventory {targetInventory.gameObject.name} to view {rootPanel.name}");
        }

        private void OnInventoryStateChanged()
        {
            if (IsOpen)
                ReloadSlots();
        }


        protected override void OnOpen()
        {
            ReloadSlots();
        }

        protected override void OnClose()
        {
            ClearSlots();
            //infoPanel.Close();
        }

        void ReloadSlots()
        {
            ClearSlots();
            CreateSlots();
            UpdateWeightText();
        }

        void CreateSlots()
        {
            var items = targetInventory.Items;

            slots = new List<ItemSlot>();
            for (int index = 0; index < items.Count; index++)
            {
                //Create slot
                var slotObject = Instantiate(slotPrefab, cellsParent);
                var slotController = slotObject.GetComponent<ItemSlot>();
                //Create item
                var itemObject = Instantiate(itemPrefab, slotObject.transform);
                var itemController = itemObject.GetComponent<ItemView>();
                
                //Init item
                itemController.Init(items[index],this,infoPanel);
                //Init slot
                slotController.Init(this, itemController);
                slots.Add(slotController);
            }
        }
        void ClearSlots()
        {
            if (slots != null)
            {
                foreach (var itemSlot in slots)
                    Destroy(itemSlot.gameObject);
            }
            slots.Clear();
        }

        void UpdateWeightText()
        {
            if (targetInventory is IWeightBased)
            {
                var weightBasedInventory = targetInventory as IWeightBased;
                
                currentWeightText.text = weightBasedInventory.Weight.ToString();
                maxWeightText.text = weightBasedInventory.MaxWeight.ToString();
            }
        }

        public bool Remove(Item item)
        {
            return targetInventory.RemoveItem(item);
        }
        

        bool GetSlotIndex(ItemSlot slot, out int index)
        {
                for (int i = 0; i < slots.Count; i++)
                {
                    if (slots[i] == slot)
                    {
                        index = i;
                        return true;
                    }
                }

            index = 0;
            return false;
        }
    }
}