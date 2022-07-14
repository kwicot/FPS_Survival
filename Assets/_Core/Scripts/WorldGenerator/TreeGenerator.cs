using UnityEngine;
using UnityEngine.Events;

namespace _Core.Scripts.WorldGeneratorCore
{
    public class TreeGenerator : IGenerator
    {
        [SerializeField] private float treeMinHeight;
        [SerializeField] private GameObject[] treePrefabs; 
        public override void Execute(object data, UnityAction OnEnd)
        {
            
        }
    }

}