using UnityEngine;

namespace Player.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private PlayerStatus status;
        [SerializeField] private Movement movement;
        
        public PlayerInput Input => input;
        public PlayerStatus Status => status;
        public Movement Movement => movement;



    }
}