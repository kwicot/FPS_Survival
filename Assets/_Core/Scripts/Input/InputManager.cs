using System;
using _Core.Scripts.Player;
using Player.Core;
using UnityEngine;
using UnityEngine.Events;

namespace _Core.Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private KeysBindingData keyBindData;

        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private InterfaceInput interfaceInput;
        [SerializeField] private CarInput carInput;

        

        public static InputManager Instance;

        public PlayerInput PlayerInput => playerInput;
        public InterfaceInput InterfaceInput => interfaceInput;

        public KeysBindingData KeyBindData => keyBindData;

        

        private InputBase currentInput;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SelectInput(playerInput);
            
            //playerInput.OnInteractPress += delegate { SelectInput(inventoryInput); };
            //playerInput.OnInteractPress += delegate { SelectInput(inventoryInput); };
            
            //EventManager.OnWindowOpen += delegate { SelectInput(inventoryInput); };
            //EventManager.OnWindowClose += delegate { SelectInput(playerInput); };

            interfaceInput.OnInventoryKeyPress += OnInterfaceOpenKeyPressed;
            interfaceInput.OnCraftKeyPress += OnInterfaceOpenKeyPressed;
            interfaceInput.OnMapKeyPress += OnInterfaceOpenKeyPressed;
            interfaceInput.OnQuestsKeyPress += OnInterfaceOpenKeyPressed;
            interfaceInput.OnSkillsKeyPress += OnInterfaceOpenKeyPressed;
            interfaceInput.OnCloseWindowPress += delegate { SelectInput(playerInput); };
        }

        private void OnInterfaceOpenKeyPressed()
        {
            if (currentInput == playerInput)
                SelectInput(interfaceInput);
        }


        void SelectInput<T>(T inputBase) where T : InputBase
        {
            if(currentInput)
                currentInput.DisableInput();
            
            currentInput = inputBase;
            currentInput.EnableInput();
        }
        

        private void Update()
        {
            
        }
    }
}