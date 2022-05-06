using System;
using UnityEngine;

namespace Player.Core
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        
        [SerializeField] private float mouseSensitivity = 100f;
        [SerializeField] private float maxAngleX;
        [SerializeField] private float minAngleX;
        [SerializeField] private Transform playerBody;

        [SerializeField] private float normalHeight;
        [SerializeField] private float crouchHeight;


        private float xRotation = 0;


        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerController.Input.PlayerInput.OnRotationInput += Rotate;
        }

        private void Update()
        {
            if (playerController.Movement.isCrouch)
                transform.localPosition = new Vector3(0, crouchHeight, 0);
            else
                transform.localPosition = new Vector3(0, normalHeight, 0);
        }

        private void Rotate(float x, float y)
        {
            var mouseX = x * mouseSensitivity * Time.deltaTime;
            var mouseY = y * mouseSensitivity * Time.deltaTime;
            
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, minAngleX, maxAngleX);
            
            transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}