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
                obj.GetComponent<ItemView>().currentSlot = this;
                
                view.Move(parent.GetComponent<ItemSlot>(), this);
            }
        }
    }
}