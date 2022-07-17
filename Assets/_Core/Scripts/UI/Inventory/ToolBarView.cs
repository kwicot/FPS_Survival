using System;
using _Core.Scripts.InventorySystem;
using _Core.Scripts.Items;
using UnityEngine;

namespace _Core.Scripts.UI
{
    public class ToolBarView : MonoBehaviour
    {
        [SerializeField] private PlayerToolBar toolBar;
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private GameObject itemViewPrefab;
        [SerializeField] private Transform slotsParent;
        
        private GameObject[] slots;


        private void Start()
        {
            ReloadSlots();
            EventManager.OnItemSelected += delegate(WeaponItem arg0) { ReloadSlots(); };
        }

        void ReloadSlots()
        {
            ClearSlots();
            CreateSlots();
            CreateItemViews();
        }

        void CreateItemViews()
        {
            Item[] items = toolBar.Items;
            for (int i = 0; i < items.Length; i++)
            {
                var item = items[i];
                if (item != null)
                {
                    var obj = Instantiate(itemViewPrefab, slots[i].transform);
                    var itemView = obj.GetComponent<ToolbarItemView>();
                    itemView.Initialize(toolBar.PlayerInventory,this,item);
                }
            }
        }
        void CreateSlots()
        {
            for (int i = 0; i < slots.Length; i++) 
            {
                var slotObject = CreateSlot(i);
                slots[i] = slotObject;
            }
        }
        GameObject CreateSlot(int index)
        {
            var obj = Instantiate(slotPrefab, slotsParent);
                var slot = obj.GetComponent<ToolbarItemSlot>();

                obj.transform.name = $"ToolbarSlot_{index}";
                slot.Initialize(toolBar.PlayerInventory,this,index,toolBar.SelectedSlot == index);
                
            return obj;
        }
        void ClearSlots()
        {
            slots ??= new GameObject[8];
            foreach (var slot in slots)
            {
                if(slot)
                    Destroy(slot.gameObject);
            }
        }

        public bool MoveToInventory(int slotIndex)
        {
            var res = toolBar.MoveToInventory(slotIndex);
            ReloadSlots();
            return res;
        }
        public bool MoveToInventory(Item item)
        {
            for (int i = 0; i < toolBar.Items.Length; i++)
            {
                var toolBarItem = toolBar.Items[i];
                if (toolBarItem == item)
                {
                    var res = toolBar.MoveToInventory(i);
                    if (res) res = RemoveItem(i);
                    ReloadSlots();
                    return res;
                }
            }

            return false;

        }
        public bool RemoveItem(int slotIndex)
        {
            var res = toolBar.RemoveItem(slotIndex);
            ReloadSlots();
            return res;
        }

        public bool AddItem(Item item, int index)
        {
            var res = toolBar.AddItem(item, index);
            if (res) toolBar.RemoveOnInventory(item);
                ReloadSlots();
            return res;
        }
    }
}