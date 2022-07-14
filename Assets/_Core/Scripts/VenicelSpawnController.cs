using System;
using UnityEngine;

namespace _Core.Scripts
{
    public class VenicelSpawnController : MonoBehaviour
    {
        [SerializeField] private GameObject carPrefab;
        [SerializeField] private Transform spawntransform;


        private void Start()
        {
            Instantiate(carPrefab, spawntransform.position, Quaternion.identity);
        }
    }
}