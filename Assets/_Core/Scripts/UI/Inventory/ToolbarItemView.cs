using _Core.Scripts.InventorySystem;
using _Core.Scripts.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Core.Scripts.UI
{
    public class ToolbarItemView : ItemViewBase, IDropHandler
    {
        public void Initialize(InventoryBase rootInventory, Item item)
        {
            base.Initialize(rootInventory,item);
            
        }
        protected override void OnClick()
        {
            
        }

        protected override void OnDoubleClick()
        {
            
        }

        public void OnDrop(PointerEventData eventData)
        {
            
        }
    }
}