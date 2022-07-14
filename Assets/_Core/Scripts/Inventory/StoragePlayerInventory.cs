using UnityEngine;

namespace _Core.Scripts.InventorySystem
{
    public class StoragePlayerInventory : PlayerInventory
    {
        [SerializeField] private PlayerInventory playerInventory;

        public PlayerInventory PlayerInventory => playerInventory;
    }
}