using System.Collections.Generic;
using _Core.Scripts.InventorySystem.Models;
using _Core.Scripts.Items;
using UnityEngine;

namespace _Core.Scripts.InventorySystem.Configs
{
    [CreateAssetMenu(fileName = "StartItemsConfig_", menuName = "Inventory/Storage/Start items config", order = 0)]
    public class StartItemsConfig : ScriptableObject
    {
        [SerializeField] private List<StartItem> startItems;

        public List<Item> GetItems()
        {
            var items = new List<Item>();
            foreach (var startItem in startItems)
            {
                var item = startItem.itemSO.Model as Item;
                item.Count = startItem.count;
                items.Add(item);
            }

            return items;
        }
    }
}