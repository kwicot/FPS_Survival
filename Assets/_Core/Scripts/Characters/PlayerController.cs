using _Core.Scripts.Input;
using UnityEngine;

namespace Player.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputManager input;
        [SerializeField] private PlayerStatus status;
        [SerializeField] private Movement movement;
        [SerializeField] private Inventory inventory;
        
        public InputManager Input => input;
        public PlayerStatus Status => status;
        public Movement Movement => movement;
        public Inventory Inventory => inventory;



    }
}