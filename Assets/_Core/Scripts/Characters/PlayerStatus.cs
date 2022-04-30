using UnityEngine;

namespace Player.Core
{
    public class PlayerStatus : MonoBehaviour
    {
        public bool CanSprint { get; set; } = true;
        public bool CanJump { get; set; } = true;
    }
}