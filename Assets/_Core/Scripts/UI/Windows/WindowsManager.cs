using System;
using System.Collections.Generic;
using System.Linq;
using _Core.Scripts.Input;
using UnityEngine;

namespace _Core.Scripts.UI.MainMenu
{
    public class WindowsManager : MonoBehaviour
    {
        [SerializeField] private WindowBase[] windows;

        public static WindowsManager Instance;
        
        private Dictionary<Type, WindowBase> windowsMap;
        
        private WindowBase currentWindow;
        private List<WindowBase> windowsHistory = new List<WindowBase>();


        private void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(this);
            
            InitializeWindowsMap();
        }

        

        void InitializeWindowsMap()
        {
            windowsMap = new Dictionary<Type, WindowBase>();

            foreach (var windowBase in windows)
            {
                windowsMap.Add(windowBase.GetType(),windowBase);
                windowBase.Close();
            } 
            
            OpenWindow<MainMenuWindow>();
        }

        public void OpenWindow<T>() where T : WindowBase
        {
            if (currentWindow)
            {
                currentWindow.Close();
                windowsHistory.Add(currentWindow);
            }
            
            currentWindow = windowsMap[typeof(T)];
            currentWindow.Open();
        }

        public void Back()
        {
            currentWindow.Close();
            
            currentWindow = windowsHistory.Last();
            currentWindow.Open();
            windowsHistory.RemoveAt(windowsHistory.Count-1);
        }

        public void CloseWindows()
        {
            if (currentWindow)
            {
                currentWindow.Close();
                windowsHistory.Add(currentWindow);
            }
            
            currentWindow = null;
        }
    }
}