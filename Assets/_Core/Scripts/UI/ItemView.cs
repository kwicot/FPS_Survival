using System;
using _Core.Scripts.Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Core.Scripts.UI
{
    public class ItemView : MonoBehaviour, IPointerClickHandler//, IPointerEnterHandler, IPointerExitHandler//IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI countText;

        private Item currentItem;
        private InventoryView invetoryView;

        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        public ItemSlot Slot { get; set; }
        public Item Item => currentItem;

        private ItemInfoPanel infoPanel;
        
        public void Init(Item item,InventoryView inventoryView, ItemInfoPanel infoPanel)
        {
            this.invetoryView = inventoryView;
            this.currentItem = item;
            this.infoPanel = infoPanel;
            itemImage.sprite = item.Image;
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            Slot = transform.parent.GetComponent<ItemSlot>();
            
            item.OnCountChanged += delegate(int arg0) { UpdateText(); };
            UpdateText();
        }


        void UpdateText()
        {
            countText.text = currentItem.Count.ToString();
        }

        public void ResetPosition()
        {
            
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Click");
        }
    }
}