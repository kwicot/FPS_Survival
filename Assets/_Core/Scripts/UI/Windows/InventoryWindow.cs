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
        [SerializeField] protected InventoryBase targetInventory;
        
        [SerializeField] protected GameObject slotPrefab;
        [SerializeField] protected GameObject itemPrefab;
        [SerializeField] protected Transform cellsParent;

        [SerializeField] protected Text currentWeightText;
        [SerializeField] protected Text maxWeightText;

        [SerializeField] protected ItemInfoPanel infoPanel;

        protected List<ItemSlotBase> slots = new List<ItemSlotBase>();
        public InventoryBase TargetInventory => targetInventory;


        private void Start()
        {
            if(targetInventory)
                targetInventory.OnStateChanged += ReloadSlots;
        }


        public bool AddItem(Item item)
        {
            if (targetInventory)
            {
                targetInventory.AddItem(item);
                return true;
            }
            else
                return false;
        }

        public bool Remove(Item item)
        {
            return targetInventory.RemoveItem(item);
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

        public void SetInventory(InventoryBase target)
        {
            targetInventory = target;
            if(targetInventory)
                targetInventory.OnStateChanged += ReloadSlots;

            //Debug.Log($"Set inventory {targetInventory.gameObject.name} to view {rootPanel.name}");
        }
        protected void ReloadSlots()
        {
            ClearSlots();
            InitializeSlots();
            UpdateWeightText();
        }
        void InitializeSlots()
        {
            var items = targetInventory.Items;
            slots = new List<ItemSlotBase>();
            
            for (int index = 0; index < items.Count; index++)
            {
                var slot = CreateSlot();
                CreateItemView(slot,items[index]);
            }

            CreateSlot();
        }
        void CreateItemView(GameObject slot,Item item)
        {

            var itemObject = Instantiate(itemPrefab, slot.transform);
            var itemController = itemObject.GetComponent<PlayerItemView>();
            itemController.Initialize(targetInventory,item,this,infoPanel);
        }
        GameObject CreateSlot()
        {
            var slotObject = Instantiate(slotPrefab, cellsParent);
            var slotController = slotObject.GetComponent<ItemSlotBase>();
            slotObject.name = $"Slot_{slots.Count}";
            slotController.Initialize(targetInventory);
            slots.Add(slotController);
            return slotObject;
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
        bool GetSlotIndex(ItemSlotBase slotBase, out int index)
        {
                for (int i = 0; i < slots.Count; i++)
                {
                    if (slots[i] == slotBase)
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