using _Core.Scripts.InventorySystem;
using _Core.Scripts.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Core.Scripts.UI
{
    public class ToolbarItemView : ItemViewBase,IDropHandler
    {
        private ToolBarView toolBar;
        public void Initialize(InventoryBase rootInventory,ToolBarView toolBar, Item item)
        {
            base.Initialize(rootInventory,item);
            this.toolBar = toolBar;

        }
        protected override void OnClick()
        {
            
        }

        protected override void OnDoubleClick()
        {
            toolBar.MoveToInventory(Item);
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log($"Drop on {gameObject.name}");
            GetComponentInParent<ToolbarItemSlot>().OnDrop(eventData);
        }
    }
}