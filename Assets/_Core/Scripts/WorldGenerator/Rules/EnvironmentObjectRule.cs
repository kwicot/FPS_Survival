using System;
using UnityEngine;

namespace _Core.Scripts.WorldGeneratorCore.Rules
{
    [Serializable]
    public class EnvironmentObjectRule
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int minCount;
        [SerializeField] private int maxCount;
        [SerializeField] private float minHeight;
        [SerializeField] private float maxHeight;

        public GameObject Prefab => prefab;
        public int MinCount => minCount;
        public int MaxCount => maxCount;
        public float MinHeight => minHeight;
        public float MaxHeight => maxHeight;
    }
}