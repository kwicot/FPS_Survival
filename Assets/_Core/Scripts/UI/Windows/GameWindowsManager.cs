using System;
using _Core.Scripts.Input;
using _Core.Scripts.InventorySystem;
using _Core.Scripts.UI.MainMenu;
using UnityEngine;
using UnityEngine.Events;

namespace _Core.Scripts.UI.Windows
{
    public class GameWindowsManager : MonoBehaviour
    {
        [SerializeField] private GameObject buttonsPanel;
        [SerializeField] private InventoryWindow inventoryWindow;
        [SerializeField] private StorageInventoryWindow storageInventoryWindow;
        [SerializeField] private CarInteractWindow carInteractWindow;
        [SerializeField] private BuildMenuWindow buildMenuWindow;
        [SerializeField] private DebugWindow debugWindow;

        private WindowBase currentWindow;

        public static GameWindowsManager Instance;

        public UnityAction OnOpen;
        public UnityAction OnClose;

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

            EventManager.OnEnterCar += delegate(CarController arg0) { CloseWindow(); };
            InputManager.Instance.InterfaceInput.OnDebugMenuKeyPress += OpenDebugWindow;

        }

        private void OnInventoryKeyPressed()
        {
            if(currentWindow == inventoryWindow)
                CloseWindow();
            else
                OpenInventory();
                
        }

        public void OpenInventory() => OpenWindow(inventoryWindow);

        public void OpenDebugWindow()
        {
            if(currentWindow == debugWindow)
                CloseWindow();
            else
                OpenWindow(debugWindow);
        }

        public void OpenBuildMenu()
        {
            if(currentWindow == buildMenuWindow)
                CloseWindow();
            else
                OpenWindow(buildMenuWindow);   
        }
        public void OpenStorageInventory(InventoryBase inventory)
        { 
            //playerInventoryWindow.SetAdditionalInventory(inventory);
            storageInventoryWindow.SetInventory(inventory);
            OpenWindow(storageInventoryWindow);
        }

        public void ShowCarDialogWindow(CarController carController)
        {
            carInteractWindow.SetCarController(carController);
            OpenWindow(carInteractWindow);
        }


        void OpenWindow(WindowBase window)
        {
            if(currentWindow)
                currentWindow.Close();
            else
                OnOpen?.Invoke();

            currentWindow = window;
            currentWindow.Open();
            buttonsPanel.SetActive(true);
        }

        public void CloseWindow()
        {
            Debug.Log("Close");
            if(currentWindow)
                currentWindow.Close();
            
            //playerInventoryWindow.SetAdditionalInventory(null);
            //storageInventoryWindow.SetWindows(null);

            currentWindow = null;
            buttonsPanel.SetActive(false);
            OnClose?.Invoke();
        }
    }
}