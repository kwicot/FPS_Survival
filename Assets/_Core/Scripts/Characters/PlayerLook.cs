using System;
using UnityEngine;

namespace Player.Core
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity = 100f;
        [SerializeField] private float maxAngleX;
        [SerializeField] private float minAngleX;
        [SerializeField] private Transform playerBody;


        private float xRotation = 0;


        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, minAngleX, maxAngleX);
            
            transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}