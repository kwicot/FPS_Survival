using _Core.Scripts.InventorySystem.Configs;
using UnityEngine;

namespace _Core.Scripts.InventorySystem
{
    public class ConfiguredStartItems : MonoBehaviour
    {
        [SerializeField] private Inventory targetInventory;
        [SerializeField] private StartItemsConfig startItemsConfig;

        private void Start()
        {
            if(!targetInventory)
            {
                Debug.LogWarning($"Target inventory is null on {gameObject.name}");
                return;
            }
            if(!startItemsConfig)
            {
                Debug.LogWarning($"Start items config is null on {gameObject.name}");
                return;
            }

            var items = startItemsConfig.GetItems();
            foreach (var item in items)
                targetInventory.AddItem(item, out var _);

        }
    }
}