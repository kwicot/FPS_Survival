using System.Collections.Generic;
using Player.Core;
using UnityEngine;

namespace _Core.Scripts.Storage
{
    public class StorageController : MonoBehaviour
    {
        [SerializeField] private Vector2Int storageSize;

        private Inventory Inventory;
    }
}