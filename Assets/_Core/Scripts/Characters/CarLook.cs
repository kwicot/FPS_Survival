using UnityEngine;

namespace Player.Core
{
    public class CarLook : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        
        [SerializeField] private Vector3 offset;

        private Vector3 rotation;
        private Vector3 position;

    }
    
}