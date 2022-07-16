using _Core.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Core.Scripts.UI
{
    public class ItemSlotBase : MonoBehaviour, IDropHandler
    {
        private InventoryBase rootInventory;

        protected ItemViewBase CurrentItem
        {
            get
            {
                if (transform.childCount == 0) return null;
                if (transform.GetChild(0).TryGetComponent(out ItemViewBase item)) return item;
                return null;
            }
        }
        
        public InventoryBase RootInventory => rootInventory;

        public void Initialize(InventoryBase rootInventory)
        {
            this.rootInventory = rootInventory;
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log($"On drop on {gameObject.name}");
            if (eventData.pointerDrag)
            {
                var obj = eventData.pointerDrag;
                if(obj.TryGetComponent(out ItemViewBase itemView))
                    ProcessDroppedItem(itemView);
            }
        }

        protected virtual void ProcessDroppedItem(ItemViewBase itemView)
        {
            
        }
        
    }
}