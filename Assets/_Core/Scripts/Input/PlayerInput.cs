using System;
using System.Collections;
using _Core.Scripts.Input;
using _Core.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 0414

namespace Player.Core
{
    public class PlayerInput : InputBase
    {
        [SerializeField] private bool isWindowsBlockMovement;
        [SerializeField] private float interactHoldTime;

        
        public KeyCode JumpKey => InputManager.Instance.KeyBindData.JumpKey;
        public KeyCode CrouchKey => InputManager.Instance.KeyBindData.CrouchKey;
        public KeyCode SprintKey => InputManager.Instance.KeyBindData.SprintKey;
        public KeyCode Attack1Key => InputManager.Instance.KeyBindData.Attack1Key;
        public KeyCode Attack2Key => InputManager.Instance.KeyBindData.Attack2Key;
        public KeyCode ReloadKey => InputManager.Instance.KeyBindData.ReloadKey;
        public KeyCode InteractKey => InputManager.Instance.KeyBindData.InteractKey;
        

        public UnityAction OnJumpPress;
        public UnityAction OnJumpHold;
        public UnityAction OnJumpRelease;
        
        public UnityAction OnCrouchPress;
        public UnityAction OnCrouchHold;
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
            if (Input.GetKeyDown(JumpKey)) OnJumpPress?.Invoke(); 
            if (Input.GetKey(JumpKey)) OnJumpHold?.Invoke();
            if (Input.GetKeyUp(JumpKey)) OnJumpRelease?.Invoke();
            
            if (Input.GetKeyDown(CrouchKey)) OnCrouchPress?.Invoke();
            if (Input.GetKey(CrouchKey)) OnCrouchHold?.Invoke();
            if (Input.GetKeyUp(CrouchKey)) OnCrouchRelease?.Invoke();
            
            if (Input.GetKeyDown(SprintKey)) OnSprintPress?.Invoke();
            if (Input.GetKeyUp(SprintKey)) OnSprintRelease?.Invoke();

            if (Input.GetKeyDown(InteractKey)) isInteractHold = true;
            if (Input.GetKeyUp(InteractKey)) isInteractHold = false;
            
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
                    //Debug.Log("Interact hold");
                    OnInteractHold?.Invoke();
                }
            }

            if (!isInteractHold && interactTime > 0)
            {
                if (interactTime < interactHoldTime)
                {
                    //Debug.Log("Interact press");
                    OnInteractPress?.Invoke();
                }
                interactTime = 0;
            }
        }
        

        void UpdateInput()
        {
            if (Input.GetKeyDown(Attack1Key)) OnAttack1Press?.Invoke();
            if (Input.GetKeyUp(Attack1Key)) OnAttack1Release?.Invoke();
            
            if (Input.GetKeyDown(Attack2Key)) OnAttack2Press?.Invoke();
            if (Input.GetKeyUp(Attack2Key)) OnAttack2Release?.Invoke();
            
            if (Input.GetKeyDown(ReloadKey)) OnReloadPress?.Invoke();
            if (Input.GetKeyUp(ReloadKey)) OnReloadRelease?.Invoke();
            
        }


        protected override void Enable()
        {
            
        }

        protected override void Disable()
        {
            
        }
    }
}