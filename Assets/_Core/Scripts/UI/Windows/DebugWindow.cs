using System;
using _Core.Scripts.Player;
using _Core.Scripts.UI.MainMenu;
using UnityEngine;

namespace _Core.Scripts.UI
{
    public class DebugWindow : WindowBase
    {
        [SerializeField] private PlayerController playerController;

        public void ChangeFlyMode(bool value)
        {
            playerController.Status.IsFlyMode = value;
        }

        public void ChangeBuildMode(bool value)
        {
            playerController.Status.IsFreeBuild = value;
        }

        public void ChangePlayerSpeed(string value)
        {
            try
            {
                var speed = System.Convert.ToSingle(value);
                playerController.Movement.ChangeBaseSpeed(speed);
            }
            catch (Exception e)
            {
                Debug.LogError($"cant change speed vie {e}");
            }
        }

        public void ChangePlayerJumpForce(string value)
        {
            try
            {
                var jumpHeight = System.Convert.ToSingle(value);
                playerController.Movement.ChangeBaseJumpForce(jumpHeight);
            }
            catch (Exception e)
            {
                Debug.LogError($"cant change speed vie {e}");
            }
        }

        protected override void OnOpen()
        {
            
        }

        protected override void OnClose()
        {
            
        }
    }
}