using System;
using _Core.Scripts.UI;
using Player.Core;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Scripts
{
    public class InteractController : MonoBehaviour
    {
        [SerializeField] private Camera lookCamera;
        [SerializeField] private float interactDistance;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private WindowsManager windowsManager;
        
        [SerializeField] private Text interactText;

        private void Start()
        {
            playerController.Input.PlayerInput.OnInteractPress += OnInteractPress;
            playerController.Input.PlayerInput.OnInteractHold += OnInteractHold;
        }

        private void FixedUpdate()
        {
            if (GetRayObject(out var obj) && windowsManager.IsOpen == false)
            {
                if (obj.TryGetComponent(out IInteractable interactable))
                    interactText.enabled = true;
                else
                    interactText.enabled = false;
            }
            else
                interactText.enabled = false;
        }

        private void OnInteractHold()
        {
            
        }

        private void OnInteractPress()
        {
            if (GetRayObject(out var obj))
            {
                if(obj.TryGetComponent(out Inventory inv))
                    windowsManager.ShowInventory(inv);
            }
        }

        bool GetRayObject(out GameObject obj)
        {
            var ray = new Ray(lookCamera.transform.position, lookCamera.transform.forward);

            if (Physics.Raycast(ray, out var hit, interactDistance))
            {
                obj = hit.transform.gameObject;
                return true;
            }

            obj = null;
            return false;
        }
    }
}