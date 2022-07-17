using System;
using _Core.Scripts.Player;
using UnityEngine;

namespace _Core.Scripts.Weapons
{
    public abstract class ItemControllerBase : MonoBehaviour
    {
        [SerializeField] protected AnimationControllerBase animationController;
        [SerializeField] protected Animator animator;
        protected PlayerController playerController;

        private void Awake()
        {
            animationController = GetComponent<AnimationControllerBase>();
            playerController = GetComponentInParent<PlayerController>();

            playerController.Input.PlayerInput.OnAttack1Press += LeftMousePress;
            playerController.Input.PlayerInput.OnAttack1Release += LeftMouseRelease;
            playerController.Input.PlayerInput.OnAttack2Press += RightMousePress;
            playerController.Input.PlayerInput.OnAttack2Release += RightMouseRelease;
        }
        protected abstract void LeftMousePress();
        protected abstract void LeftMouseRelease();
        protected abstract void RightMousePress();
        protected abstract void RightMouseRelease();
    }
}