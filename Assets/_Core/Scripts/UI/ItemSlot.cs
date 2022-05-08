using _Core.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Core.Scripts.UI
{
    public class ItemSlot : MonoBehaviour, IDropHandler
    {
        private InventoryView view;
        
        public void Init(InventoryView view)
        {
            this.view = view;
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                var obj = eventData.pointerDrag;
                var parent = obj.transform.parent;
                var itemView = obj.GetComponent<ItemView>();
                var item = itemView.Item;
                Debug.Log($"Drop {item.Name}");
                var itemSlot = itemView.Slot;
                
                var moveResult = view.Move(item,this);

                
                
                if (moveResult == MoveResult.MoveToEmpty)
                {
                    if(itemSlot.view != view)
                        itemView.ChangeInventory(view);
                    
                    itemView.Slot= this;
                }
                else if (moveResult == MoveResult.MoveToExist)
                {
                    if(itemSlot.view != view)
                        itemView.ChangeInventory(view);
                    
                    Destroy(obj);
                }
            }
        }
    }
}