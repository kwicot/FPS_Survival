using UnityEngine;

namespace _Core.Scripts.UI
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] protected GameObject rootPanel;
        [SerializeField] protected GameObject targetPanel;
        [SerializeField] protected GameObject[] additionalPanels;
        
        public abstract void Init();

        public virtual void Open()
        {
            if(rootPanel) 
                rootPanel.gameObject.SetActive(true);
            
            targetPanel.SetActive(true);
            
            foreach (var additionalPanel in additionalPanels)
                additionalPanel.SetActive(true);
        }

        public virtual void Close()
        {
            foreach (var additionalPanel in additionalPanels)
                additionalPanel.SetActive(false);
            
            if(rootPanel)
                rootPanel.SetActive(false);
            
            targetPanel.SetActive(false);
            
        }
    }
}