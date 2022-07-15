using _Core.Scripts.InventorySystem.Configs;
using UnityEngine;

namespace _Core.Scripts.InventorySystem
{
    public class RandomStartItems : MonoBehaviour
    {
        [SerializeField] private StorageInventory targetStorageInventory;
        [SerializeField] private RandomItemsConfig randomItemsConfig;

        private void Start()
        {
            if(!targetStorageInventory)
            {
                Debug.LogWarning($"Target inventory is null on {gameObject.name}");
                return;
            }
            if(!randomItemsConfig)
            {
                Debug.LogWarning($"Random items config is null on {gameObject.name}");
                return;
            }
            var items = randomItemsConfig.GetItems();
            foreach (var item in items)
                targetStorageInventory.AddItem(item);
        }
    }
}