using UnityEngine;
using UnityEngine.Events;

namespace _Core.Scripts.WorldGeneratorCore
{
    public abstract class IGenerator : MonoBehaviour
    {
        protected WorldGenerator worldGenerator;

        public void Init(WorldGenerator worldGenerator)
        {
            this.worldGenerator = worldGenerator;
        }
        public abstract void Execute(object data, UnityAction OnEnd);
    }
}