using UnityEngine;

namespace _Core.Scripts.Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected float baseDamage;
        [SerializeField] protected float baseHeath;
        [SerializeField] protected float attackInterval;

        public abstract void Attack();
    }
}