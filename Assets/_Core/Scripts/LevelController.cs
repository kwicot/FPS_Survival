using System;
using Unity.Mathematics;
using UnityEngine;

namespace _Core.Scripts
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private Transform playerSpawnTransform;
        [SerializeField] private GameObject playerPrefab;

        private GameObject player;

        public static LevelController Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            player = Instantiate(playerPrefab, playerSpawnTransform.position, quaternion.identity);
        }
    }
}