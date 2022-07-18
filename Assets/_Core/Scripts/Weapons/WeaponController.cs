using System;
using _Core.Scripts.Items;
using UnityEngine;

namespace _Core.Scripts.Weapons
{
    public class WeaponController : ItemControllerBase
    {
        [SerializeField] private WeaponConfig weaponConfig;

        private void Update()
        {
            var velocity = playerController.Movement.Velocity;
            animator.SetFloat("speed",velocity);
            Debug.Log($"velocity {velocity}");
        }

        protected override void Initialize()
        {
            
        }

        protected override void LeftMousePress()
        {
            
        }

        protected override void LeftMouseRelease()
        {
            
        }

        protected override void RightMousePress()
        {
            
        }

        protected override void RightMouseRelease()
        {
            
        }

        protected override void ReloadPressed()
        {
            
        }
    }
}