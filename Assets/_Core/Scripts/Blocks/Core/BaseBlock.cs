using System;
using System.Collections.Generic;
using _Core.Scripts.Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace Blocks.Core
{
    public class BaseBlock : UnityEngine.MonoBehaviour
    {
        [SerializeField] private string id;
        [SerializeField] private string name;
        [SerializeField] protected float startHealth;
        [SerializeField] protected BlockItem[] itemsToBuild;
        [SerializeField] protected Sprite sprite;
        [SerializeField] private bool useGlobalCell;
        [SerializeField] private BuildCellSize currentCellSize;
        [SerializeField] private Collider collider;

        private List<Item> currentItems;
        private float currentHealth;

        

        public float Health => startHealth;
        public string ID => id;
        public string Name => name;
        public bool UseGlobalCell => useGlobalCell;
        public Sprite Sprite => sprite;
        public Collider Collider => collider;

        public BlockItem[] ItemsForBuild => itemsToBuild;
        public List<Item> CurrentItems => currentItems;
        
        public BuildCellSize CurrentCellSize
        {
            get => currentCellSize;
            set => currentCellSize = value;
        }

        public bool Fill(List<Item> items)
        {
            AddItems(items);
            collider.enabled = true;
            return IsFullItems();
        }

        bool IsFullItems()
        {
            foreach (var needItem in itemsToBuild)
            {
                if (GetItem(needItem.ItemID, out var item))
                    if (item.Count < needItem.Count)
                    {
                        return false;
                    }
                    else return false;
                else return false;
            }

            return true;
        }

        void AddItems(List<Item> items)
        {
            foreach (var item in items)
            {
                if (GetItem(item.ID, out var currentItem))
                    currentItem.Count += item.Count;
                else
                    currentItems.Add(item);                    
                
            }
        }

        bool GetItem(string id,out Item needItem)
        {
            needItem = null;
            foreach (var item in currentItems)
            {
                if (item.ID == id)
                {
                    needItem = item;
                    return true;
                }
            }

            return false;
        }

    }

    public enum BuildCellSize
    {
        normal = 1,
        small = 2,
        verySmall = 4
    }
}