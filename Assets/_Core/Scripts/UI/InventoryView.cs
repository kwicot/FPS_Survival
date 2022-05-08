using System.Collections.Generic;
using _Core.Scripts.InventorySystem;
using _Core.Scripts.Items;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0472

namespace _Core.Scripts.UI
{
    public class InventoryView : Window
    {
        [SerializeField] protected GameObject slotPrefab;
        [SerializeField] protected GameObject itemPrefab;
        [SerializeField] protected Transform cellsParent;

        [SerializeField] private InventoryView additionalInventoryView;
        
        [SerializeField] private Text currentWeightText;
        [SerializeField] private Text maxWeightText;

        protected Inventory targetInventory;
        
        protected RectTransform rectTransform;
        protected List<ItemSlot> slots = new List<ItemSlot>();

        private void Start()
        {
            //playerController.Input.OnInventoryOpenKeyPressed += Open;
            //playerController.Input.OnInventoryCloseKeyPressed += Close;
            rectTransform = cellsParent.GetComponent<RectTransform>();
        }

        public override void Init()
        {
            
        }

        public void SetInventory(Inventory target)
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
                itemController.Init(items[index],this);
                //Init slot
                slotController.Init(this, itemController);
                slots.Add(slotController);
            }
            
            // //Add additional slot
            // var additionalSlotObject = Instantiate(slotPrefab, cellsParent);
            // var additionalSlotController = additionalSlotObject.GetComponent<ItemSlot>();
            // additionalSlotController.Init(this,null);
            // slots.Add(additionalSlotController);

            currentWeightText.text = targetInventory.Weight.ToString();
            maxWeightText.text = targetInventory.MaxWeight.ToString();
        }

        public bool AddItem(Item item,out AddResult result)
        {
            var isAdd = targetInventory.AddItem(item,out var res);
            result = res;
            UpdateSlots();
            return isAdd;
        }
        
        public void MoveToAdditional(ItemView itemView)
        {
            if(additionalInventoryView.IsOpen && additionalInventoryView.AddItem(itemView.Item,out var result))
            {
                if (result == AddResult.All)
                {
                    Destroy(itemView.gameObject);
                    Remove(itemView.Item);
                }
            }
            
            UpdateSlots();
        }

        public void MoveAllToAdditional()
        {
            if(additionalInventoryView.IsOpen == false) return;

            List<Item> toRemove = new List<Item>();
            foreach (var inventoryItem in targetInventory.Items)
            {
                if (additionalInventoryView.AddItem(inventoryItem, out var result))
                {
                    if (result == AddResult.All)
                        toRemove.Add(inventoryItem);
                }
                else break;
            }

            foreach (var item in toRemove)
                Remove(item);
            
            UpdateSlots();
        }

        public void Sort()
        {
            
        }
        
        // public bool AddNewItem(Item item, ItemSlot targetSlot, out AddResult result)
        // {
        //     if (GetSlotIndex(targetSlot, out var slot))
        //     {
        //         
        //     }
        // }

        // public bool Move(Item item, ItemSlot targetSlot, out MoveResult moveResult)
        // {
        //     
        // }



        // public MoveResult Move(Item item,ItemSlot to)
        // {
        //     if(GetSlotIndex(to,out var toIndex) == null) return MoveResult.Fail;
        //
        //     return (targetInventory.Move(item, toIndex));
        // }

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