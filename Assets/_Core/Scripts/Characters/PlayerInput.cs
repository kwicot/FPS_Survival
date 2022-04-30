using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player.Core
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private KeyCode jumpKey;
        [SerializeField] private KeyCode crouchKey;
        [SerializeField] private KeyCode sprintKey;
        [SerializeField] private KeyCode inventoryKey;

        
        public UnityAction OnJumpPress;
        public UnityAction OnJumpRelease;
        
        public UnityAction OnCrouchPress;
        public UnityAction OnCrouchRelease;
        
        public UnityAction OnSprintPress;
        public UnityAction OnSprintRelease;

        public UnityAction OnInventoryOpenKeyPressed;
        public UnityAction OnInventoryCloseKeyPressed;

        public bool CanLookRotation => !isInventoryOpen;

        private bool isInventoryOpen = false;
        
        
        private void Update()
        {
            if (Input.GetKeyDown(jumpKey)) OnJumpPress?.Invoke(); 
            if (Input.GetKeyUp(jumpKey)) OnJumpRelease?.Invoke();
            
            if (Input.GetKeyDown(crouchKey)) OnCrouchPress?.Invoke();
            if (Input.GetKeyUp(crouchKey)) OnCrouchRelease?.Invoke();
            
            if (Input.GetKeyDown(sprintKey)) OnSprintPress?.Invoke();
            if (Input.GetKeyUp(sprintKey)) OnSprintRelease?.Invoke();

            if (Input.GetKeyDown(inventoryKey))
            {
                if (isInventoryOpen)
                    CloseInventory();
                else
                    OpenInventory();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isInventoryOpen)
                    CloseInventory();
            }
        }

        void OpenInventory()
        {
            isInventoryOpen = true;
            Cursor.lockState = CursorLockMode.None;
            OnInventoryOpenKeyPressed?.Invoke();
        }

        void CloseInventory()
        {
            isInventoryOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            OnInventoryCloseKeyPressed?.Invoke();
        }
    }
}