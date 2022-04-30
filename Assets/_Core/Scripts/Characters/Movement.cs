using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Player.Core
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 12f;
        [SerializeField] private float jumpHeight = 3f;
        [SerializeField] private KeyCode jumpKey;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance = 0.4f;
        [SerializeField] private LayerMask groundMask;
        
        private CharacterController controller;
        private Vector3 velocity;

        private bool isGrounded;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
                velocity.y = -2f;
            
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * moveSpeed * Time.deltaTime);

            if (Input.GetKeyDown(jumpKey) && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        
    }
}
