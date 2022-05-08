using _Core.Scripts.Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Core.Scripts.UI
{
    public class ItemView : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI countText;

        private Item currentItem;
        private InventoryView invetoryView;

        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        public ItemSlot Slot { get; set; }
        public Item Item => currentItem;
        
        public void Init(Item item,InventoryView inventoryView)
        {
            this.invetoryView = inventoryView;
            this.currentItem = item;
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

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = .6f;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(Slot.transform);
            transform.localPosition = Vector3.zero;

            
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
            
            UpdateText();
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta;
        }

        public void ChangeInventory(InventoryView inventory)
        {
            invetoryView.Remove(currentItem);
            this.invetoryView = inventory;
        }
    }
}