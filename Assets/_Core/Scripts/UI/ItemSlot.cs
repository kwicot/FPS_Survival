using System;
using _Core.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Core.Scripts.UI
{
    public class ItemSlot : MonoBehaviour, IDropHandler
    {
        private InventoryWindow inventoryView;

        private ItemView CurrentItem
        {
            get
            {
                if (transform.childCount == 0) return null;
                if (transform.TryGetComponent(out ItemView item)) return item;
                return null;
            }
        }
        
        
        
        public void Init(InventoryWindow inventoryView)
        {
            this.inventoryView = inventoryView;
        }
        
        
        public void OnDrop(PointerEventData eventData)
        {
            // if (eventData.pointerDrag != null)
            // {
            //     //Get references
            //     var itemViewObject = eventData.pointerDrag;
            //     var itemView = itemViewObject.GetComponent<ItemView>();
            //     var item = itemView.Item;
            //
            //     if (inventoryView.AddItem(item, out var result))
            //     {
            //         switch (result)
            //         {
            //             case AddResult.All:
            //                 itemView.ChangeSlot(this);
            //                 break;
            //             
            //             case AddResult.Part:
            //                 break;
            //             case AddResult.Fail:
            //                 break;
            //         }
            //     }
            //             
            //     Debug.Log($"Drop {item.Name}");
            // }
        }
    }
}