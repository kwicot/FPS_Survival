using System;
using _Core.Scripts.Input;
using Player.Core;
using UnityEngine;

namespace _Core.Scripts.UI
{
    public class WindowsManager : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        
        [SerializeField] private Window playerItemsWindow;
        [SerializeField] private Window storageItemsWindow;

        private Window currentWindow;
        
        public bool IsOpen => currentWindow != null;

        private void Start()
        {
            InitWindows();
            CloseWindows();

            playerController.Input.OnCloseWindowPress += OnCloseWindowPress;
            playerController.Input.OnInventoryKeyPress += OnInventoryKeyPress;
        }

        private void OnInventoryKeyPress()
        {
            if (currentWindow == playerItemsWindow)
            {
                CloseWindows();
                return;
            }
            
            OpenWindow(playerItemsWindow);
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
            
            Cursor.lockState = CursorLockMode.None;
        }

        private void InitWindows()
        {
            playerItemsWindow.Init();
            storageItemsWindow.Init();
        }

        private void CloseWindows()
        {
            playerItemsWindow.Close();
            storageItemsWindow.Close();

            currentWindow = null;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}