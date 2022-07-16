using _Core.Scripts.InventorySystem;
using _Core.Scripts.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Core.Scripts.UI
{
    public abstract class ItemViewBase : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] protected Image itemImage;
        
        protected Item currentItem;
        protected InventoryBase rootInventory;

        private CanvasGroup canvasGroup;
        
        private float timeFromFirstClick;
        private int clicksCount = 0;
        public Item Item => currentItem;
        public ItemSlotBase ItemSlot => transform.parent.GetComponent<ItemSlotBase>(); 

        public virtual void Initialize(InventoryBase rootInventory, Item item)
        {
            this.currentItem = item;
            this.rootInventory = rootInventory;
            
            
            itemImage.sprite = item.Image;
            canvasGroup = GetComponent<CanvasGroup>();
        }
        
        private void Update()
        {
            if (clicksCount > 0)
                timeFromFirstClick += Time.deltaTime;

            if (clicksCount >= 2)
            {
                timeFromFirstClick = 0;
                clicksCount = 0;
                OnDoubleClick();
            }
            if (clicksCount == 1 && timeFromFirstClick >= 0.3f)
            {
                timeFromFirstClick = 0;
                clicksCount = 0;
                OnClick();
            }
        }

        protected abstract void OnClick();
        protected abstract void OnDoubleClick();
        
        public void OnPointerClick(PointerEventData eventData)
        {
            clicksCount++;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            offset = eventData.position - (Vector2)transform.position;
            canvasGroup.blocksRaycasts = false;
        }
        private Vector2 offset;
        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position - offset;
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            transform.localPosition = Vector3.zero;
            canvasGroup.blocksRaycasts = true;
        }
    }
}