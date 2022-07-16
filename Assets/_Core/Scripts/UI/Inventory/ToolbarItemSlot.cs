using _Core.Scripts.InventorySystem;
using _Core.Scripts.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Core.Scripts.UI
{
    public class ToolbarItemSlot : ItemSlotBase
    {
        [SerializeField] private int slotIndex;
        private ToolBarView toolBar;

        public void Initialize(InventoryBase rootInventory,ToolBarView toolBar,int slotIndex)
        {
            base.Initialize(rootInventory);
            this.toolBar = toolBar;
            this.slotIndex = slotIndex;
        }

        protected override void ProcessDroppedItem(ItemViewBase itemView)
        {
            Debug.Log($"Dropped {itemView.Item.Name}");
            var item = itemView.Item;

            if (item is WeaponItem)
            {
                Debug.Log($"Its weapon item. Current item is {CurrentItem}");
                if (CurrentItem)
                {
                    Debug.Log("Slot have item");
                    if (toolBar.MoveToInventory(slotIndex))
                    {
                        Debug.Log("Success moved");
                        toolBar.AddItem(item, slotIndex);
                    }
                }
                else
                    toolBar.AddItem(item, slotIndex);
            }
        }
    }
}