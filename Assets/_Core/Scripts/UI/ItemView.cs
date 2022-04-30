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
        [SerializeField] private TextMeshProUGUI count;

        private Item currentItem;
        private InventoryView invetoryView;

        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        public ItemSlot currentSlot;
        
        public void Init(Item item)
        {
            this.currentItem = item;
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            currentSlot = transform.parent.GetComponent<ItemSlot>();
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
            transform.SetParent(currentSlot.transform);
            transform.localPosition = Vector3.zero;

            
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta;
        }
    }
}