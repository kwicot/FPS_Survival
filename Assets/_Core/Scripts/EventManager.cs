using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Core.Scripts
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance;
        
        
        public static UnityAction OnWindowOpen;
        public static UnityAction OnWindowClose;
        
        public static UnityAction<CarController> OnEnterCar;
        public static UnityAction<CarController> OnExitCar;

        public static UnityAction OnStartCrouch;
        public static UnityAction OnStopCrouch;

        public static UnityAction OnStartSprint;
        public static UnityAction OnStopSprint;
        

        private void Awake()
        {
            if (!Instance)
                Instance = this;
            else
            {
                Destroy(this);
            }
            DontDestroyOnLoad(this);
        }
    }
    
}