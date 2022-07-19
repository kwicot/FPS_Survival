using _Core.Scripts.UI;
using Blocks.Core;
using UnityEngine;

namespace _Core.Scripts.Build
{
    [CreateAssetMenu(fileName = "New BuildConfig", menuName = "Build/Config", order = 0)]
    public class BuildConfig : ScriptableObject
    {
        [SerializeField] private BlocksHolder blocks;

        public BlocksHolder Blocks => blocks;
        
        public bool IsFreeBuild;
        public bool IsFill;
        
        public BuildCellSize CurrentCellSize;
    }
}