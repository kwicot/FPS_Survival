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
        [SerializeField] private float cellSize;

        private BlockItem[] currentItems;
        private float currentHealth;

        public string ID => id;
        public string Name => name;
        public Sprite Sprite => sprite;
        public float CellSize => cellSize;
    }
}