using UnityEngine;

namespace _Core.Scripts.InventorySystem
{
    public class StorageStorageInventory : StorageInventory
    {
        [SerializeField] private StorageInventory storageInventory;

        public StorageInventory StorageInventory => storageInventory;
    }
}