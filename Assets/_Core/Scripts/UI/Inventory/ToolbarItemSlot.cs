using _Core.Scripts.InventorySystem;
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
        
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                var itemViewObject = eventData.pointerDrag;
                //var itemView = 
            }
        }
        
    }
}