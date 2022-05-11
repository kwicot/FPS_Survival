using System;
using UnityEngine;

namespace _Core.Scripts.Items
{
    [Serializable]
    public class WeaponItem : Item
    {
        [SerializeField] private float baseDamage;
        [SerializeField] AmmoType ammoType; 
        [SerializeField] 
        public WeaponItem(string id, string name, float basePrice, bool canStack, float weight, Sprite image, int count = 1) : base(id, name, basePrice, canStack, weight, image, count)
        {
        }
    }
}