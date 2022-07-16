using System;
using UnityEngine;

namespace _Core.Scripts.Items
{
    [Serializable]
    public class WeaponItem : Item
    {
        [SerializeField] private float baseDamage;
        [SerializeField] AmmoType ammoType;

        public override object Clone(bool copyCount = false) => new WeaponItem(id, name, basePrice,canStack,baseDamage,ammoType, weight, image, copyCount ? count : 1);

        public WeaponItem(
            string id,
            string name,
            float basePrice,
            bool canStack,
            float baseDamage,
            AmmoType ammoType,
            float weight,
            Sprite image,
            int count = 1)
            : base(id, name, basePrice, canStack, weight, image, count)
        {
            this.baseDamage = baseDamage;
            this.ammoType = ammoType;
        }
    }
}