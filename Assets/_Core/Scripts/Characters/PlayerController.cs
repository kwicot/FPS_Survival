using UnityEngine;

namespace Player.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private PlayerStatus status;
        
        public PlayerInput Input => input;
        public PlayerStatus Status => status;

        
        
    }
}