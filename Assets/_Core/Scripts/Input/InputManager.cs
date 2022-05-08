using _Core.Scripts.Player;
using Player.Core;
using UnityEngine;
using UnityEngine.Events;

namespace _Core.Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private InventoryInput inventoryInput;
        [SerializeField] private CarInput carInput;

        [SerializeField] private KeyCode inventoryKey;
        [SerializeField] private KeyCode craftKey;
        [SerializeField] private KeyCode questsKey;
        [SerializeField] private KeyCode skillKey;
        [SerializeField] private KeyCode mapKey;

        public PlayerInput PlayerInput => playerInput;
        public InventoryInput InventoryInput => inventoryInput;

        public UnityAction OnInventoryKeyPress;
        public UnityAction OnCraftKeyPress;
        public UnityAction OnQuestsKeyPress;
        public UnityAction OnSkillsKeyPress;
        public UnityAction OnMapKeyPress;
        public UnityAction OnCloseWindowPress;

        private InputBase currentInput;
        private void Start()
        {
            SelectInput(playerInput);
            
            //playerInput.OnInteractPress += delegate { SelectInput(inventoryInput); };
            //playerInput.OnInteractPress += delegate { SelectInput(inventoryInput); };
            
            EventManager.OnWindowOpen += delegate { SelectInput(inventoryInput); };
            EventManager.OnWindowClose += delegate { SelectInput(playerInput); };
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
            if (UnityEngine.Input.GetKeyDown(inventoryKey))
            {
                if (currentInput == inventoryInput)
                {
                    SelectInput(playerInput);
                    OnInventoryKeyPress?.Invoke();
                }
                else
                {
                    OnInventoryKeyPress?.Invoke();
                    SelectInput(inventoryInput);
                }
            }


            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                if (currentInput != playerInput)
                {
                    SelectInput(playerInput);
                    OnCloseWindowPress?.Invoke();
                }
                else
                {
                    //Open menu
                }
            }
            
        }
    }
}