using System;
using _Core.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;

namespace _Core.Scripts.Input
{
    public class InterfaceInput : InputBase
    {

        public KeyCode InventoryKey => InputManager.Instance.KeyBindData.OpenInventoryKey;
        public KeyCode TakeAllKey => InputManager.Instance.KeyBindData.TakeAllItemsKey;
        public KeyCode BuildMenukey => InputManager.Instance.KeyBindData.OpenBuildMenuKey;
        public KeyCode DebugMenuKey => InputManager.Instance.KeyBindData.OpenDebugMenuKey;

        
        public UnityAction OnTakeAllKeyPress;
        
        public UnityAction OnInventoryKeyPress;
        public UnityAction OnCraftKeyPress;
        public UnityAction OnQuestsKeyPress;
        public UnityAction OnSkillsKeyPress;
        public UnityAction OnMapKeyPress;

        public UnityAction OnBuildMenuKeyPress;
        public UnityAction OnDebugMenuKeyPress;

        public UnityAction OnCloseWindowPress;

        private void Update()
        {
            if(UnityEngine.Input.GetKeyDown(InventoryKey)) OnInventoryKeyPress?.Invoke();
            if(UnityEngine.Input.GetKeyDown(BuildMenukey)) OnBuildMenuKeyPress?.Invoke(); 
            if(UnityEngine.Input.GetKeyDown(DebugMenuKey)) OnDebugMenuKeyPress?.Invoke();



            if(!IsEnable) return;
            
            if(UnityEngine.Input.GetKeyDown(KeyCode.Escape)) OnCloseWindowPress?.Invoke();
            //if(UnityEngine.Input.GetKeyDown(takeAllKey)) OnTakeAllKeyPress?.Invoke();
            
        }

        protected override void Enable()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        protected override void Disable()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}