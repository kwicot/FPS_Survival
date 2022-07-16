using _Core.Scripts.InventorySystem;
using _Core.Scripts.Items;
using _Core.Scripts.UI.Windows;
using TMPro;
using UnityEngine;

namespace _Core.Scripts.UI
{
    public class PlayerItemView : ItemViewBase//, IPointerEnterHandler, IPointerExitHandler//IPointerDownHandler, 
    {
        
        [SerializeField] private TextMeshProUGUI countText;

        
        private InventoryWindow invetoryWindow;


        private ItemInfoPanel infoPanel;


        public void Initialize(InventoryBase rootInventory, Item item, InventoryWindow window,ItemInfoPanel infoPanel)
        {
            base.Initialize(rootInventory, item);
            this.invetoryWindow = window;
            this.infoPanel = infoPanel;
            UpdateText();
        }


        void UpdateText()
        {
            countText.text = currentItem.Count.ToString();
        }

        

        public void ChangeSlot(ItemSlotBase newSlotBase)
        {
            transform.SetParent(newSlotBase.transform);
            transform.localPosition = Vector3.zero;
        }


        protected override void OnClick()
        {
            
        }

        protected override void OnDoubleClick()
        {
            if (invetoryWindow is StorageInventoryWindow)
            {
                if ((invetoryWindow as StorageInventoryWindow).MoveToAdditional(Item))
                {
                    invetoryWindow.Remove(Item);
                }
            }
        }
    }
}