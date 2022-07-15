using UnityEngine;

namespace _Core.Scripts.UI.MainMenu
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] protected GameObject rootPanel;
        [SerializeField] private WindowBase additionalWindow;
        
        public bool IsOpen { get; set; }

        public void Open()
        {
            rootPanel.SetActive(true);
            IsOpen = true;
            OnOpen();
            additionalWindow?.Open();
        }

        public void Close()
        {
            IsOpen = false;
            
            OnClose();
            rootPanel.SetActive(false);
            additionalWindow?.Close();
            
        }

        public void Back()
        {
            WindowsManager.Instance.Back();
        }

        protected abstract void OnOpen();
        protected abstract void OnClose();
    }
}