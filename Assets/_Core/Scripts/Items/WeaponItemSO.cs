using UnityEngine;

namespace _Core.Scripts.Items
{
    [CreateAssetMenu(fileName = "New WeaponItem", menuName = "Items/Weapon", order = 0)]
    public class WeaponItemSO : ScriptableObject
    {
        [SerializeField] protected WeaponItem model;

        public object Model => model.Clone();
    }
}