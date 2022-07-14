using System;
using UnityEngine;

namespace _Core.Scripts.WorldGeneratorCore.Rules
{
    [Serializable]
    public class MapGenerationRules
    {
        [SerializeField] private int seed;
        [SerializeField] private int octaves;
        [SerializeField] private float persistence;
        [SerializeField] private float lacunarity;
        [SerializeField] private float noiseScale;
        [SerializeField] private int biomeGrid;
        [SerializeField] private float noiseMult;
        [SerializeField] private float noiseDist;
        [SerializeField] private Vector2 offset;



        public int Seed => seed;
        public int Octaves => octaves;
        public int BiomeGrid => biomeGrid;
        public float NoiseDist => noiseDist;
        public float Persistence => persistence;
        public float Lacunarity => lacunarity;
        public float NoiseMult => noiseMult;
        public float NoiseScale => noiseScale;
        public Vector2 Offset => offset;
    }
}