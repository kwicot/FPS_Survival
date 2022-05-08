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
        
        [SerializeField] private InventoryView inventoryWindow;
        [SerializeField] private StorageInventoryView storageItemsWindow;
        [SerializeField] private CarInteractWindow carInteractWindow;
        
        

        private Window currentWindow;

        public bool IsOpen => currentWindow != null;
        

        private void Start()
        {
            InitWindows();
            CloseWindows();

            playerController.Input.OnCloseWindowPress += OnCloseWindowPress;
            playerController.Input.OnInventoryKeyPress += OnInventoryKeyPress;
        }

        public void ShowCarInteractWindow()
        {
            OpenWindow(carInteractWindow);
        }



        public void ShowStorageInventory(Inventory inventory)
        {
            storageItemsWindow.SetInventory(inventory);
            inventoryWindow.SetInventory(playerController.Inventory);
            
            OpenWindow(storageItemsWindow);
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
            EventManager.OnWindowOpen?.Invoke();
            Cursor.lockState = CursorLockMode.None;
        }
        private void InitWindows()
        {
            inventoryWindow.Init();
            storageItemsWindow.Init();
        }
        public void CloseWindows()
        {
            if(currentWindow)
                currentWindow.Close();
            
            inventoryWindow.Close();
            storageItemsWindow.Close();
            carInteractWindow.Close();

            currentWindow = null;
            Cursor.lockState = CursorLockMode.Locked;
            playerDot.SetActive(true);
            EventManager.OnWindowClose?.Invoke();
        }
    }
}