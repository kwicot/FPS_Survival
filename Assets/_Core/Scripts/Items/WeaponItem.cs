using System;
using UnityEngine;

namespace _Core.Scripts.Items
{
    [Serializable]
    public class WeaponItem : Item
    {
        [SerializeField] private WeaponConfig weaponConfig;
        public WeaponConfig WeaponConfig => weaponConfig;

        public override object Clone(bool copyCount = false) => new WeaponItem(id, name, basePrice,canStack,weaponConfig, weight, image, copyCount ? count : 1);

        public WeaponItem(
            string id,
            string name,
            float basePrice,
            bool canStack,
            WeaponConfig weaponConfig,
            float weight,
            Sprite image,
            int count = 1)
            : base(id, name, basePrice, canStack, weight, image, count)
        {
            this.weaponConfig = weaponConfig;
        }
    }
}