using System;
using _Core.Scripts.Items;

namespace _Core.Scripts.InventorySystem.Models
{
    [Serializable]
    public class StartItem
    {
        public ItemSO itemSO;
        public int count;
    }
}