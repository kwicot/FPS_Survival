using System;
using System.Collections;
using _Core.Scripts.InventorySystem;
using _Core.Scripts.Items;
using _Core.Scripts.UI.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Core.Scripts.UI
{
    public class ItemView : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler//, IPointerEnterHandler, IPointerExitHandler//IPointerDownHandler, 
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI countText;

        private Item currentItem;
        private InventoryWindow invetoryWindow;

        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        public InventoryItemSlot Slot { get; set; }
        public Item Item => currentItem;

        private ItemInfoPanel infoPanel;

        private float timeFromFirstClick;
        private int clicksCount = 0;
        
        public void Init(Item item,InventoryWindow inventoryWindow, ItemInfoPanel infoPanel)
        {
            this.invetoryWindow = inventoryWindow;
            this.currentItem = item;
            this.infoPanel = infoPanel;
            itemImage.sprite = item.Image;
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            Slot = transform.parent.GetComponent<InventoryItemSlot>();
            
            item.OnCountChanged += delegate(int arg0) { UpdateText(); };
            UpdateText();
        }


        void UpdateText()
        {
            countText.text = currentItem.Count.ToString();
        }

        private void Update()
        {
            if (clicksCount > 0)
                timeFromFirstClick += Time.deltaTime;

            if (clicksCount >= 2)
            {
                timeFromFirstClick = 0;
                clicksCount = 0;
                ProcessDoubleClick();
            }
            if (clicksCount == 1 && timeFromFirstClick >= 0.3f)
            {
                timeFromFirstClick = 0;
                clicksCount = 0;
                ProcessClick();
            }
        }

        public void ChangeSlot(InventoryItemSlot newSlot)
        {
            transform.SetParent(newSlot.transform);
            transform.localPosition = Vector3.zero;
        }


        void ProcessClick()
        {
        }

        void ProcessDoubleClick()
        {
            if (invetoryWindow is StorageInventoryWindow)
            {
                if ((invetoryWindow as StorageInventoryWindow).MoveToAdditional(Item))
                {
                        invetoryWindow.Remove(Item);
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            clicksCount++;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            offset = eventData.position - (Vector2)transform.position;
        }


        private Vector2 offset;
        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position + offset;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.localPosition = Vector3.zero;
        }
    }
}