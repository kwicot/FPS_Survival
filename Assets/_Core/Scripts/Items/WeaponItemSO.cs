using UnityEngine;

namespace _Core.Scripts.Items
{
    [CreateAssetMenu(fileName = "New WeaponItem", menuName = "Items/Weapon", order = 0)]
    public class WeaponItemSO : ItemSO
    {
        [SerializeField] protected Item model;

        public override object Model => model.Clone();
    }
}