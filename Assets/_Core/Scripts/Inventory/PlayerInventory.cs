using System.Collections.Generic;
using _Core.Scripts.Items;
using UnityEngine;

namespace _Core.Scripts.InventorySystem
{
    public class PlayerInventory : StorageInventory
    {

        [SerializeField] private int toolBarSlotsCount;
        [SerializeField] private PlayerToolBar toolBar;

        public override float Weight
        {
            get
            {
                var weight = 0f;
                foreach (var item in itemsList)
                    weight += item.TotalWeight;

                weight += toolBar.Weight;
                return weight;
            }
        }
        
    }
}