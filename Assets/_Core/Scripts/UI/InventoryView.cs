using System;
using System.Security.Cryptography;
using Player.Core;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace _Core.Scripts.UI
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;

        [SerializeField] private GameObject panel;
        
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private GameObject itemPrefab;
        
        [SerializeField] private Transform cellsParent;

        private RectTransform rectTransform;
        private ItemSlot[,] slots;

        private bool isOpen;
        private bool initialized;

        private void Start()
        {
            playerController.Input.OnInventoryOpenKeyPressed += Open;
            playerController.Input.OnInventoryCloseKeyPressed += Close;
            rectTransform = cellsParent.GetComponent<RectTransform>();
            panel.SetActive(false);
        }

        public void Open()
        {
            if(!initialized)
                InitSlots();
                
            panel.SetActive(true);
            InitItems();
            
            isOpen = true;
        }

        void Close()
        {
            panel.SetActive(false);
            //Destroy items
            foreach (var cell in slots)
                if(cell.transform.childCount > 0)
                    Destroy(cell.transform.GetChild(0).gameObject);

            isOpen = false;
        }

        void InitSlots()
        {
            var items = playerController.Inventory.Items;
            var rows = items.GetLength(0);
            var columns = items.GetLength(1);

            var panelWidth = rectTransform.rect.width;
            var spacingX = gridLayoutGroup.spacing.x;
            var paddingLeft = gridLayoutGroup.padding.left;
            var paddingRight = gridLayoutGroup.padding.right;
            var totalSpacing = spacingX * (columns - 1);
            var widthWithoutSpacing = panelWidth - totalSpacing - paddingLeft - paddingRight;
            var cellSize = widthWithoutSpacing / columns;

            Debug.Log($"Width = {panelWidth}, columns = {columns}, spacingX = {spacingX}, total spacing = {totalSpacing}");

            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayoutGroup.constraintCount = columns;
            gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);

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

        void InitItems()
        {
            var items = playerController.Inventory.Items;
            for (int row = 0; row < items.GetLength(0); row++)
            {
                for (int column = 0; column < items.GetLength(1); column++)
                {
                    if (items[row, column] != null)
                    {
                        var itemObject = Instantiate(itemPrefab, slots[row, column].transform);
                        itemObject.GetComponent<ItemView>().Init(items[row,column]);
                    }
                }
            }
        }

        public void Move(ItemSlot from, ItemSlot to)
        {
            Debug.Log($"Move from {from.gameObject.name}, to {to.gameObject.name}");
            if(GetSlotIndex(from,out var fromIndex) == false) return;
            if(GetSlotIndex(to,out var toIndex) == null) return;

            playerController.Inventory.Move(fromIndex, toIndex);
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