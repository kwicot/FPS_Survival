using _Core.Scripts.InventorySystem;
using _Core.Scripts.Items;
using UnityEngine;

#pragma warning disable 0472

namespace _Core.Scripts.UI
{
    public class InventoryView : Window
    {
        [SerializeField] protected GameObject slotPrefab;
        [SerializeField] protected GameObject itemPrefab;
        
        [SerializeField] protected Transform cellsParent;

        protected Inventory targetInventory;
        
        protected RectTransform rectTransform;
        protected ItemSlot[,] slots = new ItemSlot[1,1];

        protected bool isOpen;
        protected bool initialized;

        private void Start()
        {
            //playerController.Input.OnInventoryOpenKeyPressed += Open;
            //playerController.Input.OnInventoryCloseKeyPressed += Close;
            rectTransform = cellsParent.GetComponent<RectTransform>();
        }

        public override void Init()
        {
            
        }

        public void SetInventory(Inventory target)
        {
            targetInventory = target;
            Debug.Log($"Set inventory {targetInventory.gameObject.name} to view {targetPanel.name}");
            InitSlots();
        }
        public override void Open()
        {
            base.Open();
            InitItems();
            targetPanel.SetActive(true);
            isOpen = true;
        }

        public override void Close()
        {
            //Destroy items
            foreach (var cell in slots)
                if(cell != null && cell.transform.childCount > 0)
                    Destroy(cell.transform.GetChild(0).gameObject);

            targetPanel.SetActive(false);
            isOpen = false;
            
            base.Close();
        }

        private void InitSlots()
        {
            var childs = cellsParent.childCount;
            for (int i = childs - 1; i >= 0; i--)
                Destroy(cellsParent.GetChild(i).gameObject);
            
            var items = targetInventory.Items;
            var rows = items.GetLength(0);
            var columns = items.GetLength(1);

            slots = new ItemSlot[rows, columns];
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    var cellObject = Instantiate(slotPrefab, cellsParent);
                    var slotController = cellObject.GetComponent<ItemSlot>();
                    slotController.Init(this);
                    slots[row, column] = slotController;
                }
            }

            initialized = true;
        }

        private void InitItems()
        {
            var items = targetInventory.Items;
            for (int row = 0; row < items.GetLength(0); row++)
            {
                for (int column = 0; column < items.GetLength(1); column++)
                {
                    if (items[row, column] != null)
                    {
                        var itemObject = Instantiate(itemPrefab, slots[row, column].transform);
                        itemObject.GetComponent<ItemView>().Init(items[row,column],this);
                    }
                }
            }
        }

        public MoveResult Move(Item item,ItemSlot to)
        {
            if(GetSlotIndex(to,out var toIndex) == null) return MoveResult.Fail;

            return (targetInventory.Move(item, toIndex));
        }

        public bool Remove(Item item)
        {
            return targetInventory.RemoveItem(item);
        }
        

        bool GetSlotIndex(ItemSlot slot, out Vector2Int index)
        {
            for (int row = 0; row < slots.GetLength(0); row++)
            {
                for (int column = 0; column < slots.GetLength(1); column++)
                {
                    if (slots[row, column] == slot)
                    {
                        index = new Vector2Int(row, column);
                        return true;
                    }
                }
            }

            index = Vector2Int.zero;
            return false;
        }
    }
}