using UnityEngine;

namespace _Core.Scripts.Build
{
    [CreateAssetMenu(fileName = "BlocksData", menuName = "Build/Blocks data", order = 0)]
    public class BlocksHolder : ScriptableObject
    {
        [SerializeField] private GameObject[] blocks;
        public GameObject[] AllBlocks => blocks;
    }
}