using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Core.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        
        
        [SerializeField] private float jumpHeight = 3f;
        [SerializeField] private KeyCode jumpKey;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance = 0.4f;
        [SerializeField] private LayerMask groundMask;
        
        [Header("Movement")]
        [SerializeField] private float crouchSpeed = 2;
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float sprintSpeed = 10;

        [Header("Collider")] 
        [SerializeField] private float normalHeight;
        [SerializeField] private float crouchHeight;

        
        private CharacterController controller;
        private Vector3 velocity;
        private float speed;
        private Vector3 positionOnLastFrame;
        
        
        public bool isGrounded => Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        public bool isSprint => speed == sprintSpeed;
        public bool isCrouch => controller.height == crouchHeight;
        

        public UnityAction OnSprint;
        public UnityAction OnJump;

        float Speed => speed / playerController.Inventory.Overweight;

        private float JumpHeight => jumpHeight / playerController.Inventory.Overweight;

        public float Velocity { get ; private set; }


        private void Start()
        {
            controller = GetComponent<CharacterController>();
            
            playerController.Input.PlayerInput.OnJumpPress += Jump;
            playerController.Input.PlayerInput.OnJumpRelease += JumpHoldRelease;
            playerController.Input.PlayerInput.OnCrouchPress += StartCrouch;
            playerController.Input.PlayerInput.OnCrouchHold += OnCrouchHold;
            playerController.Input.PlayerInput.OnCrouchRelease += StopCrouch;
            playerController.Input.PlayerInput.OnSprintPress += StartSprint;
            playerController.Input.PlayerInput.OnSprintRelease += StopSprint;
            
            playerController.Input.PlayerInput.OnMoveInput += Move;

            speed = moveSpeed;
        }

        


        private void Move(float horizontal, float vertical)
        {
            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            controller.Move(move * Speed * Time.deltaTime);
        }

        private void StopSprint()
        {
            if(!isSprint) return;

            speed = moveSpeed;
            EventManager.OnStopSprint?.Invoke();
        }

        private void StartSprint()
        { 
            if(isCrouch) return;
            if(!playerController.Status.CanSprint) return;
            
            speed = sprintSpeed;
            EventManager.OnStartSprint?.Invoke();
        }

        private void StopCrouch()
        {
            if (playerController.Status.IsFlyMode)
            {
                velocity.y = 0;
                return;
            }
            if(!isCrouch) return;
            
            controller.height = normalHeight;
            controller.center = new Vector3(0, normalHeight / 2, 0);
            speed = moveSpeed;
            EventManager.OnStopCrouch?.Invoke();
        }

        private void OnCrouchHold()
        {
            if (playerController.Status.IsFlyMode)
                velocity.y = -speed;
        }
        
        private void StartCrouch()
        {
            if(playerController.Status.IsFlyMode) return;
            if(isSprint) return;
            
            controller.height = crouchHeight;
            controller.center = new Vector3(0, crouchHeight / 2, 0);
            speed = crouchSpeed;
            EventManager.OnStartCrouch?.Invoke();
        }

        private void Jump()
        {
            if (playerController.Status.IsFlyMode)
            {
                velocity.y = speed;
                return;
            }          
            if(!playerController.Status.CanJump) return;
            if(!isGrounded) return;
            if(isCrouch) return;

            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
            OnJump?.Invoke();
        }
        
        private void JumpHoldRelease()
        {
            if (playerController.Status.IsFlyMode)
                velocity.y = 0;
        }

        private void Update()
        {
            if (!playerController.Status.IsFlyMode)
            {

                if (isGrounded && velocity.y < 0)
                    velocity.y = -2f;

                if (isSprint && playerController.Status.Stamina <= 0)
                    StopSprint();

                velocity.y += gravity * Time.deltaTime * playerController.Inventory.Overweight;
            }

            controller.Move(velocity * Time.deltaTime);

            if (isSprint) OnSprint?.Invoke();
        }
        
        #region Debug

        public void ChangeBaseSpeed(float value)
        {
            moveSpeed = value;
            sprintSpeed = value * 1.6666f;
            
            speed = moveSpeed;
        }

        public void ChangeBaseJumpForce(float value)
        {
            jumpHeight = value;
        }

        #endregion

    }
}
