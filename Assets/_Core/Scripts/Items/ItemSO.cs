using UnityEngine;

namespace _Core.Scripts.Items
{
    [CreateAssetMenu(menuName = "Items", fileName = "New Item")]
    public class ItemSO : ScriptableObject
    {
        [SerializeField] private Item model;
        
        public Item Model => model.Clone() as Item;
    }
}