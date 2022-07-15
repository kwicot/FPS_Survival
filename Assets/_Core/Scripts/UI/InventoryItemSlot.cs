using System;
using _Core.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Core.Scripts.UI
{
    public class InventoryItemSlot : MonoBehaviour, IDropHandler
    {
        private InventoryWindow inventoryView;

        private ItemView CurrentItem
        {
            get
            {
                if (transform.childCount == 0) return null;
                if (transform.GetChild(0).TryGetComponent(out ItemView item)) return item;
                return null;
            }
        }
        
        
        
        public void Init(InventoryWindow inventoryView)
        {
            this.inventoryView = inventoryView;
        }
        
        
        public void OnDrop(PointerEventData eventData)
        {
            // Debug.Log($"Drop event on slot {gameObject.name}");
            // //if(CurrentItem != null) return;
            //
            // if (eventData.pointerDrag != null)
            // {
            //     
            //     //Get references
            //     var itemViewObject = eventData.pointerDrag;
            //     var itemView = itemViewObject.GetComponent<ItemView>();
            //     var item = itemView.Item;
            //
            //     if (inventoryView.AddItem(item))
            //         Destroy(eventData.pointerDrag);
            //             
            //     Debug.Log($"Drop {item.Name}");
            // }
        }
    }
}