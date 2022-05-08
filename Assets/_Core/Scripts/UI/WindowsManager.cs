using System.Collections.Generic;
using _Core.Scripts.InventorySystem;
using _Core.Scripts.Player;
using _Core.Scripts.UI.InteractWindows;
using UnityEngine;

namespace _Core.Scripts.UI
{
    public class WindowsManager : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private GameObject playerDot;

        [SerializeField] private InventoryView playerInventoryWindow;
        [SerializeField] private InventoryView StorageInventoryWindow;
        [SerializeField] private Window PlayerEquipmentWindow;
        
        [SerializeField] private CarInteractWindow carInteractWindow;
        
        

        private List<Window> lastOpenWindows;
        

        public bool IsOpen => lastOpenWindows.Count > 0;
        

        private void Start()
        {
            lastOpenWindows = new List<Window>();
            lastOpenWindows.Add(playerInventoryWindow);
            lastOpenWindows.Add(StorageInventoryWindow);
            lastOpenWindows.Add(carInteractWindow);
            lastOpenWindows.Add(PlayerEquipmentWindow);
            
            InitWindows();
            CloseWindows();

            playerController.Input.OnCloseWindowPress += OnCloseWindowPress;
            playerController.Input.OnInventoryKeyPress += OnInventoryKeyPress;
        }

        public void ShowCarInteractWindow()
        {
            OpenWindow(carInteractWindow);
        }

        void OpenWindow(Window window)
        {
            playerDot.SetActive(false);
            Cursor.lockState = CursorLockMode.None;

            window.Open();
            lastOpenWindows.Add(window);
            EventManager.OnWindowOpen?.Invoke();
            
        }


        public void ShowStorageInventory(Inventory inventory)
        {
            CloseWindows();
            
            StorageInventoryWindow.SetInventory(inventory);
            playerInventoryWindow.SetInventory(playerController.Inventory);
            
            OpenWindow(playerInventoryWindow);
            OpenWindow(StorageInventoryWindow);
        }


        private void OnInventoryKeyPress()
        {
            CloseWindows();
            
            if (playerInventoryWindow.IsOpen)
            {
                CloseWindows();
                return;
            }
            
            playerInventoryWindow.SetInventory(playerController.Inventory);
            OpenWindow(playerInventoryWindow);
            OpenWindow(PlayerEquipmentWindow);
        }
        private void OnCloseWindowPress()
        {
            CloseWindows();
        }
        private void InitWindows()
        {
            playerInventoryWindow.Init();
            StorageInventoryWindow.Init();
        }
        public void CloseWindows()
        {
            foreach (var lastOpenWindow in lastOpenWindows)
                lastOpenWindow.Close();
            lastOpenWindows.Clear();
            
            Cursor.lockState = CursorLockMode.Locked;
            playerDot.SetActive(true);
            EventManager.OnWindowClose?.Invoke();
        }
    }
}