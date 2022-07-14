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

        protected override void OnOpen()
        {
            
        }

        protected override void OnClose()
        {
            
        }
    }
}