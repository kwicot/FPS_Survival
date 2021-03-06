using System;
using _Core.Scripts.Items;
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

        public static UnityAction<WeaponItem> OnItemSelected;
        public static UnityAction<string> OnBlockSelected;


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