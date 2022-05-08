using System;
using System.Collections;
using UnityEngine;

#pragma warning disable 0414

namespace _Core.Scripts.TerrainGeneration
{
    public class TerrainGenerator : MonoBehaviour
    {
        [Header("Ohter Components")]
        [SerializeField] ChunksGenerator chunkGenerator;
        
        [Header("Noise")]
        [SerializeField] private int width = 256;
        [SerializeField] private int height = 256;
        [SerializeField] private float depth = 20;
        [SerializeField] private float scale = 20;
        
        [SerializeField] private Vector2 offset;


        private void Start()
        {
            Init();
        }

        void Init()
        {
            //chunkGenerator.Generate();
        }


        private float[,] GenerateHights(Vector3 chunkOffset)
        {
            float[,] heights = new float[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    heights[x, y] = CalculateHeight(x + chunkOffset.x, y + chunkOffset.y);
                }
            }

            return heights;
        }

        private float CalculateHeight(float x, float y)
        {
            float xCord = x / width * scale + offset.x;
            float yCord = y / height * scale + offset.y;

            return Mathf.PerlinNoise(xCord, yCord);
        }

    }
}