using UnityEngine;

namespace _Core.Scripts.InventorySystem
{
    public class StorageInventory : Inventory
    {
        [SerializeField] private Inventory inventory;

        public Inventory Inventory => inventory;
    }
}