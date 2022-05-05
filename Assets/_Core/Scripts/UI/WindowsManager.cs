using System;
using _Core.Scripts.Input;
using Player.Core;
using UnityEngine;
using UnityEngine.Events;

namespace _Core.Scripts.UI
{
    public class WindowsManager : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private GameObject playerDot;
        
        [SerializeField] private InventoryView inventoryWindow;
        [SerializeField] private StorageInventoryView storageItemsWindow;
        [SerializeField] private Window interactWindow;
        

        private Window currentWindow;
        
        public bool IsOpen => currentWindow != null;
        

        private void Start()
        {
            InitWindows();
            CloseWindows();

            playerController.Input.OnCloseWindowPress += OnCloseWindowPress;
            playerController.Input.OnInventoryKeyPress += OnInventoryKeyPress;
        }


        public void ShowInventory(Inventory inventory)
        {
            storageItemsWindow.SetInventory(inventory);
            inventoryWindow.SetInventory(playerController.Inventory);
            
            OpenWindow(storageItemsWindow);
        }

        public void ShowInteractWindow()
        {
            
        }


        private void OnInventoryKeyPress()
        {
            if (currentWindow == inventoryWindow)
            {
                CloseWindows();
                return;
            }
            
            inventoryWindow.SetInventory(playerController.Inventory);
            OpenWindow(inventoryWindow);
        }
        private void OnCloseWindowPress()
        {
            CloseWindows();
        }
        void OpenWindow<T>(T window) where T : Window
        {
            if(currentWindow)
                currentWindow.Close();

            currentWindow = window;
            currentWindow.Open();
            
            playerDot.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
        private void InitWindows()
        {
            inventoryWindow.Init();
            storageItemsWindow.Init();
        }
        private void CloseWindows()
        {
            inventoryWindow.Close();
            storageItemsWindow.Close();

            currentWindow = null;
            Cursor.lockState = CursorLockMode.Locked;
            playerDot.SetActive(true);
        }
    }
}