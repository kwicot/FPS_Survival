using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player.Core
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private KeyCode jumpKey;
        [SerializeField] private KeyCode crouchKey;
        [SerializeField] private KeyCode sprintKey;

        public UnityAction OnJumpPress;
        public UnityAction OnJumpRelease;
        
        public UnityAction OnCrouchPress;
        public UnityAction OnCrouchRelease;
        
        public UnityAction OnSprintPress;
        public UnityAction OnSprintRelease;
        
        private void Update()
        {
            if (Input.GetKeyDown(jumpKey)) OnJumpPress?.Invoke(); 
            if (Input.GetKeyUp(jumpKey)) OnJumpRelease?.Invoke();
            
            if (Input.GetKeyDown(crouchKey)) OnCrouchPress?.Invoke();
            if (Input.GetKeyUp(crouchKey)) OnCrouchRelease?.Invoke();
            
            if (Input.GetKeyDown(sprintKey)) OnSprintPress?.Invoke();
            if (Input.GetKeyUp(sprintKey)) OnSprintRelease?.Invoke();
        }
    }
}