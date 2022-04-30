using UnityEngine;

namespace Player.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private PlayerStatus status;
        [SerializeField] private Movement movement;
        [SerializeField] private Inventory inventory;
        
        public PlayerInput Input => input;
        public PlayerStatus Status => status;
        public Movement Movement => movement;
        public Inventory Inventory => inventory;



    }
}