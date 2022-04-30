using System;
using UnityEngine;

namespace Player.Core
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

        public float Stamina { get; private set; }
        public float Health { get; private set; }

        public float MaxStamina => maxBaseStamina;
        public float MaxHealth => maxBaseHealth;
        

        public bool CanSprint => Stamina > 0;
        public bool CanJump => Stamina > jumpBaseUseStamina;
        

        private void Start()
        {
            playerController.Movement.OnSprint += OnSprint;
            playerController.Movement.OnJump += OnJump;

            Stamina = MaxStamina;
            Health = MaxHealth;
        }

        private void OnJump()
        {
            Stamina -= jumpBaseUseStamina;
        }

        private void OnSprint()
        {
            Stamina -= sprintBaseUseStamina * Time.deltaTime;
        }

        private void Update()
        {
            Regen();
        }

        void Regen()
        {
            if (Stamina < MaxStamina)
            {
                Stamina += staminaBaseRegenSpeed * Time.deltaTime;
                if (Stamina > MaxStamina) Stamina = MaxStamina;
            }

            if (Health < MaxHealth)
            {
                Health += healthBaseRegenSpeed * Time.deltaTime;
                if (Health > maxBaseHealth) Health = MaxHealth;
            }
        }

        
        
    }
}