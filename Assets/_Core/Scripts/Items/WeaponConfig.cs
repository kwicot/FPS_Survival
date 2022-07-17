using UnityEngine;

namespace _Core.Scripts.Items
{
    [CreateAssetMenu(fileName = "New WeaponConfig", menuName = "Items/Weapon config", order = 0)]
    public class WeaponConfig : ScriptableObject
    {
        [Header("Object properties")]
        [SerializeField] private GameObject weaponPrefab;
        [SerializeField] private Vector3 spawnPosition;
        
        [Header("Weapon properties")]
        [SerializeField] private float baseDamage;
        [SerializeField] AmmoType ammoType;

        public Vector3 SpawnPosition => spawnPosition;
        public GameObject WeaponPrefab => weaponPrefab;
        
        public float BaseDamage => baseDamage;
        public AmmoType AmmoType => ammoType;
    }
}