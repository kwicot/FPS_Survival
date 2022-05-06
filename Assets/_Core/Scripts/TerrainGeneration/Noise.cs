using TreeEditor;
using UnityEngine;

namespace _Core.Scripts.TerrainGeneration
{
    public class Noise
    {
        public float GetHeight(Vector3 position)
        {
            return new Perlin().Noise(position.x, position.y, position.z);
        }
    }
}