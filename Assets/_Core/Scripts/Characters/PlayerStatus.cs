using System;
using _Core.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace _Core.Scripts.Player
{
    public class PlayerStatus : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [Header("Base params")]
        [SerializeField] private float maxBaseStamina;
        [SerializeField] private float maxBaseHealth;

        [Header("Base uses value")]
        [SerializeField] private float sprintBaseUseStamina;
        [SerializeField] private float jumpBaseUseStamina;

        [Header("Base regen value")]
        [SerializeField] private float staminaBaseRegenSpeed;
        [SerializeField] private float healthBaseRegenSpeed;

        private bool isFlyMode;

        public float Stamina { get; private set; }
        public float Health { get; private set; }

        public float MaxStamina => maxBaseStamina;
        public float MaxHealth => maxBaseHealth;
        

        public bool CanSprint => Stamina > 0;
        public bool CanJump => Stamina > jumpBaseUseStamina;
        
        public bool InCar { get; set; }
        public bool IsBuildMode { get; set; }
        public bool IsFreeBuild { get; set; }

        public bool IsFlyMode
        {
            get => isFlyMode;
            set
            {
                isFlyMode = value;
                OnFlyModeChanged?.Invoke(value);
            }
        }

        public CarController currentCar { get; set; }

        public UnityEvent<bool> OnFlyModeChanged = new UnityEvent<bool>(); 
        

        public enum PlayerWindow
        {
            Game,
            Inventory
        }
        

        private void Start()
        {
            playerController.Movement.OnSprint += OnSprint;
            playerController.Movement.OnJump += OnJump;
            EventManager.OnEnterCar += OnEnterCar;
            EventManager.OnExitCar += OnExitCar;

            Stamina = MaxStamina;
            Health = MaxHealth;
        }

        private void OnExitCar(CarController car)
        {
            InCar = false;
            currentCar = null;
        }

        private void OnEnterCar(CarController car)
        {
            currentCar = car;
            InCar = true;
        }

        private void OnJump()
        {
            Stamina -= jumpBaseUseStamina * playerController.Inventory.Overweight;
        }

        private void OnSprint()
        {
            Stamina -= sprintBaseUseStamina * Time.deltaTime * playerController.Inventory.Overweight;
        }

        private void Update()
        {
            Regen();
        }

        void Regen()
        {
                Stamina += staminaBaseRegenSpeed * Time.deltaTime / playerController.Inventory.Overweight;
                if (Stamina > MaxStamina) Stamina = MaxStamina;
                if (Stamina < 0) Stamina = 0;

                Health += healthBaseRegenSpeed * Time.deltaTime;
                if (Health > maxBaseHealth) Health = MaxHealth;
        }

        
        
    }
}