using _Core.Scripts.InventorySystem;
using _Core.Scripts.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Core.Scripts.UI
{
    public class ItemSlot : MonoBehaviour, IDropHandler
    {
        private InventoryWindow targetInventoryWindow;
        private ItemView currentItem;
        
        public void Init(InventoryWindow window, ItemView itemView)
        {
            this.targetInventoryWindow = window;
            if (itemView == null) return;
            
            currentItem = itemView;
        }
        
        
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                //Get references
                var itemViewObject = eventData.pointerDrag;
                var itemView = itemViewObject.GetComponent<ItemView>();
                var item = itemView.Item;
                
                Debug.Log($"Drop {item.Name}");
            }
        }
    }
}