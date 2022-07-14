using System;
using _Core.Scripts.Items;
using UnityEngine;

namespace Blocks.Core
{
    [Serializable]
    public class BlockItem
    {
        [SerializeField] private ItemSO itemModel;
        [SerializeField] private int count;

        public ItemSO Model => itemModel;
        public int Count
        {
            get => count;
            set => count = value;
        }
    }
}