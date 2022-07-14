using UnityEngine;

namespace _Core.Scripts.UI.MainMenu
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private GameObject rootPanel;

        public void Open()
        {
            rootPanel.SetActive(true);
            OnOpen();
        }

        public void Close()
        {
            OnClose();
            rootPanel.SetActive(false);
        }

        public void Back()
        {
            WindowsManager.Instance.Back();
        }

        protected abstract void OnOpen();
        protected abstract void OnClose();
    }
}