using UnityEngine;

namespace _Core.Scripts.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
    public class ItemSO : ScriptableObject
    {
        [SerializeField] protected Item model;
        
        public virtual object Model => model.Clone();
    }
}