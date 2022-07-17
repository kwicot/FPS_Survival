using System;
using _Core.Scripts.Items;
using _Core.Scripts.UI.Windows;
using UnityEngine;

namespace _Core.Scripts.Weapons
{
    public class BuilderController : ItemControllerBase
    {
        private WeaponConfig builderConfig;
        
        
        protected override void Initialize()
        {
            playerController.Input.InterfaceInput.OnOpenBuildMenuKeyPress += OpenBuildMenu;
            EventManager.OnBlockSelected += OnSelectblock;
        }

        private void OnSelectblock(string arg0)
        {
            
        }

        private void OpenBuildMenu()
        {
            GameWindowsManager.Instance.OpenBuildMenu();
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
    }
}