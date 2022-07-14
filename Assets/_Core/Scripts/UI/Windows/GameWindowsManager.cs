using System;
using _Core.Scripts.Input;
using _Core.Scripts.InventorySystem;
using _Core.Scripts.UI.MainMenu;
using UnityEngine;

namespace _Core.Scripts.UI.Windows
{
    public class GameWindowsManager : MonoBehaviour
    {
        [SerializeField] private InventoryWindow playerInventoryWindow;
        [SerializeField] private InventoryWindow storageInventoryWindow;

        private WindowBase currentWindow;

        public static GameWindowsManager Instance;

        private void Awake()
        {
            if (!Instance)
                Instance = this;
            else Destroy(this);
        }

        private void Start()
        {
            InputManager.Instance.InterfaceInput.OnCloseWindowPress += CloseWindow;
            InputManager.Instance.InterfaceInput.OnInventoryKeyPress += OnInventoryKeyPressed;
        }

        private void OnInventoryKeyPressed()
        {
            if(currentWindow == playerInventoryWindow)
                CloseWindow();
            else
                OpenInventory();
                
        }

        public void OpenInventory() => OpenWindow(playerInventoryWindow);
        public void OpenStorageInventory(InventoryBase inventory)
        { 
            storageInventoryWindow.SetInventory(inventory);
            OpenWindow(storageInventoryWindow);
        }

        void OpenWindow(WindowBase window)
        {
            if(currentWindow)
                currentWindow.Close();

            currentWindow = window;
            currentWindow.Open();
        }

        private void CloseWindow()
        {
            if(currentWindow)
                currentWindow.Close();

            currentWindow = null;
        }
    }
}