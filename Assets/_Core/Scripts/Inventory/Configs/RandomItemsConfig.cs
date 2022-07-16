using System.Collections.Generic;
using _Core.Scripts.InventorySystem.Models;
using _Core.Scripts.Items;
using _Core.Scripts.UI;
using UnityEngine;

namespace _Core.Scripts.InventorySystem.Configs
{
    [CreateAssetMenu(fileName = "RandomItemsConfig_", menuName = "Inventory/Storage/Random items config", order = 0)]
    public class RandomItemsConfig : ScriptableObject
    {
        [SerializeField] private List<RandomItem> randomItems;
        [SerializeField][Min(0)] private int minItems;
        [SerializeField] private int maxItems;
        [SerializeField][Min(1)] private int mixCount;

        public List<Item> GetItems()
        {
            var items = GenerateItems();
            
            MixList(items);
            SelectRandom(items);
            GenerateRandomCount(items);
            ClearNulls(items);
            
            return items;
        }

        List<Item> GenerateItems()
        {
            var list = new List<Item>();
            foreach (var randomItem in randomItems)
            {
                var count = randomItem.Change;
                for (int i = 0; i < count; i++)
                    list.Add(randomItem.ItemSo.Model as Item);
            }
            return list;
        }

        void MixList(List<Item> origin)
        {
            var min = 0;
            var max = origin.Count;
            for (int step = 0; step < mixCount; step++)
            {
                for (int i = 0; i < max; i++)
                {
                    var first = Random.Range(min, max);
                    var second = Random.Range(min, max);
                    (origin[first], origin[second]) = (origin[second], origin[first]);
                }
            }
        }

        void SelectRandom(List<Item> origin)
        {
            var items = new List<Item>();
            var count = Random.Range(minItems, maxItems + 1);
            for (int i = 0; i < count; i++)
            {
                var randomIndex = Random.Range(0, origin.Count);
                items.Add(origin[randomIndex]);
                origin.RemoveAt(randomIndex);
            }

            origin.Clear();
            origin.AddRange(items);
        }

        void GenerateRandomCount(List<Item> origin)
        {
            foreach (var item in origin)
            {
                if (GetRange(item, out int min, out int max))
                    item.Count = Random.Range(min, max+1);
                else
                    item.Count = 0;
            }
        }

        void ClearNulls(List<Item> origin)
        {
            for (int i = origin.Count - 1; i >= 0; i--)
            {
                if(origin.Count <= 0)
                    origin.RemoveAt(i);
            }
        }

        bool GetRange(Item item, out int min, out int max)
        {
            foreach (var randomItem in randomItems)
            {
                
                if ((randomItem.ItemSo.Model as Item).ID == item.ID)
                {
                    min = randomItem.MinCount;
                    max = randomItem.MaxCount;
                    return true;
                }
            }

            min = 0;
            max = 0;
            return false;
        }



    }
}