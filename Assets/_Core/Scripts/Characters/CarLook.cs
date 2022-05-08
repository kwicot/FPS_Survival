using UnityEngine;

#pragma warning disable 0108
namespace _Core.Scripts.Player
{
    public class CarLook : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        
        [SerializeField] private Vector3 offset;

        private Vector3 rotation;
        private Vector3 position;

    }
    
}