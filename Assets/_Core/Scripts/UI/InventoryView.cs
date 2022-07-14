using System.Collections.Generic;
using _Core.Scripts.InventorySystem;
using _Core.Scripts.Items;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0472

namespace _Core.Scripts.UI
{
    public class InventoryView : GameWindow
    {
        [SerializeField] protected GameObject slotPrefab;
        [SerializeField] protected GameObject itemPrefab;
        [SerializeField] protected Transform cellsParent;

        [SerializeField] private Text currentWeightText;
        [SerializeField] private Text maxWeightText;

        [SerializeField] private ItemInfoPanel infoPanel;

        private List<ItemSlot> slots = new List<ItemSlot>();

        private InventoryBase targetInventory;

        private void Start()
        {
            
        }

        public override void Init()
        {
            
        }

        public void SetInventory(PlayerInventory target)
        {
            targetInventory = target;
            Debug.Log($"Set inventory {targetInventory.gameObject.name} to view {targetPanel.name}");
        }

        private void OnInventoryStateChanged()
        {
            if (IsOpen)
                UpdateSlots();
        }

        public override void Open()
        {
            base.Open();
            
            UpdateSlots();
            targetPanel.SetActive(true);
        }

        public override void Close()
        {
            //Destroy items
            foreach (var cell in slots)
                if(cell != null && cell.transform.childCount > 0)
                    Destroy(cell.transform.GetChild(0).gameObject);

            targetPanel.SetActive(false);
            infoPanel.Close();
            
            base.Close();
        }

        protected void UpdateSlots()
        {
            // var childs = cellsParent.childCount;
            // for (int i = childs - 1; i >= 0; i--)
            //     Destroy(cellsParent.GetChild(i).gameObject);
            if (slots != null)
            {
                foreach (var itemSlot in slots)
                    Destroy(itemSlot.gameObject);
            }
            
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

            if (targetInventory is IWeightBased)
            {
                var weightBasedInventory = targetInventory as IWeightBased;
                
                currentWeightText.text = weightBasedInventory.Weight.ToString();
                maxWeightText.text = weightBasedInventory.MaxWeight.ToString();
            }
            
        }

        public bool AddItem(Item item,out AddResult result)
        {
            var isAdd = targetInventory.AddItem(item,out var res);
            result = res;
            UpdateSlots();
            return isAdd;
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