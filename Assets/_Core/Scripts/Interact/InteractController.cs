using _Core.Scripts.InventorySystem;
using _Core.Scripts.Player;
using _Core.Scripts.UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Core.Scripts
{
    public class InteractController : MonoBehaviour
    {
        [SerializeField] private Camera lookCamera;
        [SerializeField] private float interactDistance;
        [SerializeField] private PlayerController playerController;
        [FormerlySerializedAs("windowsManager")] [SerializeField] private GameWindowsManager gameWindowsManager;
        [SerializeField] private LayerMask interactLayer;
        
        [SerializeField] private Text interactText;
        

        private CarController targetCar;
        private PlayerInventory targetPlayerInventory;

        private void Start()
        {
            playerController.Input.PlayerInput.OnInteractPress += OnInteractPress;
            playerController.Input.PlayerInput.OnInteractHold += OnInteractHold;
        }

        private void FixedUpdate()
        {
            if (GetRayObject(out var obj) && gameWindowsManager.IsOpen == false)
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
            if (GetRayObject(out var obj))
            {
                if (obj.TryGetComponent(out CarController controller))
                {
                    targetCar = controller;
                    if (obj.TryGetComponent(out PlayerInventory inventory))
                        targetPlayerInventory = inventory;
                    
                    gameWindowsManager.ShowCarInteractWindow();
                }
            }
        }

        private void OnInteractPress()
        {
            if (playerController.Status.InCar)
            {
                EventManager.OnExitCar?.Invoke(playerController.Status.currentCar);
                Debug.Log("Exit car");
                return;
            }
            if (GetRayObject(out var obj))
            {
                
                if (obj.TryGetComponent(out CarController controller))
                {
                    EnterInCar(controller);
                    return;
                }
                
                
                if(obj.TryGetComponent(out PlayerInventory inv))
                    gameWindowsManager.ShowStorageInventory(inv);
            }
        }

        bool GetRayObject(out GameObject obj)
        {
            var ray = new Ray(lookCamera.transform.position, lookCamera.transform.forward);

            if (Physics.Raycast(ray, out var hit, interactDistance,interactLayer))
            {
                obj = hit.transform.gameObject;
                return true;
            }

            obj = null;
            return false;
        }

        public void EnterInCar(CarController controller)
        {
            EventManager.OnEnterCar?.Invoke(controller);
            gameWindowsManager.CloseWindows();
            Debug.Log("Enter car");
        }

        public void EnterInCar() => EnterInCar(targetCar);

        public void ShowCarInventory()
        {
            gameWindowsManager.ShowStorageInventory(targetPlayerInventory);
        }
    }
}