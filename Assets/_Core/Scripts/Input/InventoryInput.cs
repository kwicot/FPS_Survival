using System;
using _Core.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;

namespace _Core.Scripts.Input
{
    public class InventoryInput : InputBase
    {
        [SerializeField] private KeyCode takeAllKey;

        public UnityAction OnTakeAllKeyPress;

        private void Update()
        {
            if(!IsEnable) return;
            
            if(UnityEngine.Input.GetKeyDown(takeAllKey)) OnTakeAllKeyPress?.Invoke();
        }

        protected override void Enable()
        {
            
        }

        protected override void Disable()
        {
            
        }
    }
}