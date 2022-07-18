using System;
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
        [SerializeField] private BuildCellSize cellStep;

        private BlockItem[] currentItems;
        private float currentHealth;

        public string ID => id;
        public string Name => name;
        public Sprite Sprite => sprite;
        public BuildCellSize CellSizeDevide => cellStep;

    }

    public enum BuildCellSize
    {
        normal = 1,
        small = 2,
        verySmall = 4
    }
}