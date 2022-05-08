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
        [SerializeField] private GameObject infoPrefab;
        [SerializeField] private float hoverTimeInfo;

        private Item currentItem;
        private InventoryView invetoryView;

        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        public ItemSlot Slot { get; set; }
        public Item Item => currentItem;

        private GameObject infoPanel;
        
        private bool isMouseHover = false;
        private float hoverTime = 0;
        
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

        private void Update()
        {
            // if (isMouseHover && !infoPanel)
            // {
            //     hoverTime += Time.deltaTime;
            //     if (hoverTime >= hoverTimeInfo)
            //     {
            //         infoPanel = Instantiate(infoPrefab, invetoryView.transform);
            //         infoPanel.GetComponent<ItemInfoPanel>().Init(Item);
            //         hoverTime = 0;
            //         isMouseHover = false;
            //     }
            // }
            //
            // if (infoPanel)
            //     infoPanel.transform.position = UnityEngine.Input.mousePosition + new Vector3(10, 10, 0);
        }

        // public void OnPointerDown(PointerEventData eventData)
        // {
        //     
        // }
        //
        // public void OnBeginDrag(PointerEventData eventData)
        // {
        //     Debug.Log("OnBeginDrag");
        //     
        //     canvasGroup.blocksRaycasts = false;
        //     canvasGroup.alpha = .6f;
        // }
        //
        // public void OnEndDrag(PointerEventData eventData)
        // {
        //     transform.SetParent(Slot.transform);
        //     transform.localPosition = Vector3.zero;
        //
        //     
        //     canvasGroup.blocksRaycasts = true;
        //     canvasGroup.alpha = 1;
        //     
        //     UpdateText();
        // }
        //
        // public void OnDrag(PointerEventData eventData)
        // {
        //     rectTransform.anchoredPosition += eventData.delta;
        // }
        //
        // public void ChangeInventory(InventoryView inventory)
        // {
        //     invetoryView.Remove(currentItem);
        //     this.invetoryView = inventory;
        // }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (UnityEngine.Input.GetKey(KeyCode.LeftShift))
                invetoryView.MoveToAdditional(this);
        }

        // public void OnPointerEnter(PointerEventData eventData)
        // {
        //     hoverTime = 0;
        //     isMouseHover = true;
        // }
        //
        // public void OnPointerExit(PointerEventData eventData)
        // {
        //     isMouseHover = false;
        //     if (infoPanel)
        //         Destroy(infoPanel);
        // }
    }
}