using _Core.Scripts;
using _Core.Scripts.Input;
using _Core.Scripts.InventorySystem;
using UnityEngine;

namespace _Core.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerStatus status;
        [SerializeField] private Movement movement;
        [SerializeField] private PlayerInventory playerInventory;
        
        [SerializeField] private PlayerLook playerCamera;
        [SerializeField] private CarLook carCamera;
        [SerializeField] private CharacterController characterController;
        
        private InputManager input => InputManager.Instance;
        
        public InputManager Input => input;
        public PlayerStatus Status => status;
        public Movement Movement => movement;
        public PlayerInventory PlayerInventory => playerInventory;
        public PlayerLook PlayerLook => playerCamera;

        private void Start()
        {
            EventManager.OnEnterCar += OnEnterCar;
            EventManager.OnExitCar += OnExitrCar;
        }

        private void OnExitrCar(CarController car)
        {
            playerCamera.gameObject.SetActive(true);
            carCamera.gameObject.SetActive(false);

            transform.SetParent(null,true);
            transform.position = car.ExitPosition;
            car.PlayerExit();

            Movement.enabled = true;
            characterController.enabled = true;
        }

        private void OnEnterCar(CarController arg0)
        {
            playerCamera.gameObject.SetActive(false);
            carCamera.gameObject.SetActive(true);

            transform.SetParent(arg0.gameObject.transform, true);
            transform.position = arg0.PlayerPosition;
            arg0.PlayerEnter();
            
            Movement.enabled = false;
            characterController.enabled = false;

        }
    }
}