using System;
using UnityEngine;

namespace _Core.Scripts.TerrainGeneration
{
    [Serializable]
    public class NoiseGenerator
    {
        private Noise noise = new Noise();
        [SerializeField] private Vector2 offset;
        [SerializeField] private float scale = 1;
        [SerializeField] private float height = 1;
        [SerializeField] 

        public float GetHeight(int x, int y)
        {
            float xCoord = (x + offset.x) * scale;
            float yCoord = (y + offset.y) * scale;
            var point = new Vector2(xCoord, yCoord);
            return noise.Evaluate(point) * height;
        }
    }
}