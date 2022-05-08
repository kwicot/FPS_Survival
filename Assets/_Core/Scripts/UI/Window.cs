using UnityEngine;

namespace _Core.Scripts.UI
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] protected GameObject targetPanel;
        [SerializeField] private GameObject rootPanel;
        
        
        public abstract void Init();
        public bool IsOpen { get; private set; }

        public virtual void Open()
        {
            if(rootPanel) 
                rootPanel.SetActive(true);
            targetPanel.SetActive(true);
            
            IsOpen = true;
        }

        public virtual void Close()
        {
            targetPanel.SetActive(false);
            if(rootPanel)
                rootPanel.SetActive(false);
            
            IsOpen = false;
        }
    }
}