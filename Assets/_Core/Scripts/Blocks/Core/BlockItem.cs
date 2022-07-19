using System;
using _Core.Scripts.Items;
using UnityEngine;

namespace Blocks.Core
{
    [Serializable]
    public class BlockItem
    {
        [SerializeField] private ItemSO itemSo;
        [SerializeField] private int count;

        public ItemSO ItemSO => itemSo;
        public string ItemID => (itemSo.Model as Item)?.ID;
        public int Count
        {
            get => count;
            set => count = value;
        }
    }
}