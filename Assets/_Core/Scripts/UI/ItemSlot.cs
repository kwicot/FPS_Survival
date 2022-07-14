using _Core.Scripts.InventorySystem;
using _Core.Scripts.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Core.Scripts.UI
{
    public class ItemSlot : MonoBehaviour, IDropHandler
    {
        private InventoryView targetInventoryView;
        private ItemView currentItem;
        
        public void Init(InventoryView view, ItemView itemView)
        {
            this.targetInventoryView = view;
            if (itemView == null) return;
            
            currentItem = itemView;
        }
        
        
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                //Get references
                var itemViewObject = eventData.pointerDrag;
                var startItemViewParent = itemViewObject.transform.parent;
                var itemView = itemViewObject.GetComponent<ItemView>();
                var item = itemView.Item;
                var startItemViewSlot = itemView.Slot;
                
                Debug.Log($"Drop {item.Name}");
            }
        }
    }
}