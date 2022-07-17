using _Core.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Core.Scripts.UI
{
    public class ToolbarItemSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private int slotIndex;
        private PlayerToolBar toolBar;

        public void Init(PlayerToolBar toolBar)
        {
            this.toolBar = toolBar;
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