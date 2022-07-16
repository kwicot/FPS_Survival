using _Core.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Core.Scripts.UI
{
    public class ItemSlotBase : MonoBehaviour, IDropHandler
    {
        private InventoryBase rootInventory;
        private PlayerItemView CurrentItem
        {
            get
            {
                if (transform.childCount == 0) return null;
                if (transform.GetChild(0).TryGetComponent(out PlayerItemView item)) return item;
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
            
        }
    }
}