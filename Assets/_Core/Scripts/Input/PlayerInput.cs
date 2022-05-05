using System;
using System.Collections;
using _Core.Scripts.Input;
using _Core.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Player.Core
{
    public class PlayerInput : InputBase
    {
        [SerializeField] private bool isWindowsBlockMovement;
        [SerializeField] private float interactHoldTime;
        [SerializeField] private KeyCode jumpKey;
        [SerializeField] private KeyCode crouchKey;
        [SerializeField] private KeyCode sprintKey;
        [SerializeField] private KeyCode attack1Key;
        [SerializeField] private KeyCode attack2Key;
        [SerializeField] private KeyCode reloadKey;
        [SerializeField] private KeyCode interactKey;
        
        public UnityAction OnJumpPress;
        public UnityAction OnJumpRelease;
        
        public UnityAction OnCrouchPress;
        public UnityAction OnCrouchRelease;
        
        public UnityAction OnSprintPress;
        public UnityAction OnSprintRelease;

        public UnityAction OnAttack1Press;
        public UnityAction OnAttack1Release;

        public UnityAction OnAttack2Press;
        public UnityAction OnAttack2Release;

        public UnityAction OnReloadPress;
        public UnityAction OnReloadRelease;

        public UnityAction OnInteractPress;
        public UnityAction OnInteractHold;

        public UnityAction<float,float> OnMoveInput;
        public UnityAction<float, float> OnRotationInput;

        private bool isInventoryOpen = false;
        private bool isInteractHold;
        private float interactTime;

        private void Update()
        {
            if (IsEnable)
            {
                UpdateInput();
                UpdateMovementInput();
            }
            else if(!isWindowsBlockMovement)
                UpdateMovementInput();
        }

        void UpdateMovementInput()
        {
            if (Input.GetKeyDown(jumpKey)) OnJumpPress?.Invoke(); 
            if (Input.GetKeyUp(jumpKey)) OnJumpRelease?.Invoke();
            
            if (Input.GetKeyDown(crouchKey)) OnCrouchPress?.Invoke();
            if (Input.GetKeyUp(crouchKey)) OnCrouchRelease?.Invoke();
            
            if (Input.GetKeyDown(sprintKey)) OnSprintPress?.Invoke();
            if (Input.GetKeyUp(sprintKey)) OnSprintRelease?.Invoke();

            if (Input.GetKeyDown(interactKey)) isInteractHold = true;
            if (Input.GetKeyUp(interactKey)) isInteractHold = false;
            
            //Move input
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            if(horizontal != 0 || vertical != 0)
                OnMoveInput?.Invoke(horizontal,vertical);
            
            //Mouse input
            var mouseX = Input.GetAxis("Mouse X");
            var mouseY = Input.GetAxis("Mouse Y");
            if(mouseX != 0 || mouseY != 0)
                OnRotationInput?.Invoke(mouseX,mouseY);

            if (isInteractHold)
            {
                interactTime += Time.deltaTime;
                if (interactTime > interactHoldTime)
                {
                    isInteractHold = false;
                    Debug.Log("Interact hold");
                    OnInteractHold?.Invoke();
                }
            }

            if (!isInteractHold && interactTime > 0)
            {
                if (interactTime < interactHoldTime)
                {
                    Debug.Log("Interact press");
                    OnInteractPress?.Invoke();
                }
                interactTime = 0;
            }
        }

        void UpdateInput()
        {
            if (Input.GetKeyDown(attack1Key)) OnAttack1Press?.Invoke();
            if (Input.GetKeyUp(attack1Key)) OnAttack1Release?.Invoke();
            
            if (Input.GetKeyDown(attack2Key)) OnAttack2Press?.Invoke();
            if (Input.GetKeyUp(attack2Key)) OnAttack2Release?.Invoke();
            
            if (Input.GetKeyDown(reloadKey)) OnReloadPress?.Invoke();
            if (Input.GetKeyUp(reloadKey)) OnReloadRelease?.Invoke();
        }


        protected override void Enable()
        {
            
        }

        protected override void Disable()
        {
            
        }
    }
}