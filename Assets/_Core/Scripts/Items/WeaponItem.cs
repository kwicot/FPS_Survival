using System;
using UnityEngine;

namespace _Core.Scripts.Items
{
    [Serializable]
    public class WeaponItem : Item
    {
        [SerializeField] private float baseDamage;
        [SerializeField] AmmoType ammoType;

        public override object Clone(bool copyCount = false)
        {
            var clone = base.Clone() as WeaponItem;
            clone.baseDamage = baseDamage;
            clone.ammoType = ammoType;
            return clone;
        }

        public WeaponItem(string id, string name, float basePrice, bool canStack,bool canGrab, float weight, Sprite image, int count = 1) : base(id, name, basePrice, canStack, weight, image, count)
        {
        }
    }
}