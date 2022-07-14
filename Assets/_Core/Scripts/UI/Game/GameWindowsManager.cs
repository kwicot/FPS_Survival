using System.Collections.Generic;
using _Core.Scripts.InventorySystem;
using _Core.Scripts.Player;
using _Core.Scripts.UI.InteractWindows;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Core.Scripts.UI
{
    public class GameWindowsManager : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private GameObject playerDot;

        [SerializeField] private InventoryView playerInventoryWindow;
        [SerializeField] private InventoryView StorageInventoryWindow;
        [FormerlySerializedAs("PlayerEquipmentWindow")] [SerializeField] private GameWindow playerEquipmentGameWindow;
        
        [FormerlySerializedAs("carInteractWindow")] [SerializeField] private CarInteractGameWindow carInteractGameWindow;
        
        

        private List<GameWindow> lastOpenWindows;
        

        public bool IsOpen => lastOpenWindows.Count > 0;
        

        private void Start()
        {
            lastOpenWindows = new List<GameWindow>();
            lastOpenWindows.Add(playerInventoryWindow);
            lastOpenWindows.Add(StorageInventoryWindow);
            lastOpenWindows.Add(carInteractGameWindow);
            lastOpenWindows.Add(playerEquipmentGameWindow);
            
            InitWindows();
            CloseWindows();

            playerController.Input.OnCloseWindowPress += OnCloseWindowPress;
            playerController.Input.OnInventoryKeyPress += OnInventoryKeyPress;
        }

        public void ShowCarInteractWindow()
        {
            OpenWindow(carInteractGameWindow);
        }

        void OpenWindow(GameWindow gameWindow)
        {
            playerDot.SetActive(false);
            Cursor.lockState = CursorLockMode.None;

            gameWindow.Open();
            lastOpenWindows.Add(gameWindow);
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
            OpenWindow(playerEquipmentGameWindow);
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