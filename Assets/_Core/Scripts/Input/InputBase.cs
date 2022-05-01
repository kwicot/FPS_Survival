using UnityEngine;

namespace _Core.Scripts.Input
{
    public abstract class InputBase : MonoBehaviour
    {
        [SerializeField] protected InputManager manager;
        public bool IsEnable { get; private set; }

        public void DisableInput()
        {
            Debug.Log($"Disable input {gameObject.name}");
            Disable();
            IsEnable = false;
        }

        public void EnableInput()
        {
            Debug.Log($"Enable input {gameObject.name}");
            Enable();
            IsEnable = true;
        }

        protected abstract void Enable();
        protected abstract void Disable();
    }
}