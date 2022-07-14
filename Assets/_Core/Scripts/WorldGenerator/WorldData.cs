using System;
using _Core.Scripts.WorldGeneratorCore.Rules;
using UnityEngine;

namespace _Core.Scripts.WorldGeneratorCore
{
    [Serializable]
    public class WorldData
    {
        [SerializeField] private MapSize mapSize;
        [SerializeField] private int chunkSize;
        [SerializeField] private int chunkHeight;
        [SerializeField] private bool smoothTerrain;
        [SerializeField] private bool flatShaded;
        [SerializeField] private MapGenerationRules mapGenerationRules;
        [SerializeField] private BiomeType[] biomes;
        
        private Chunk[,] chunksData;
        private float[,] heightMap;
        private int[,] biomeMap;
        
        
        public Chunk[,] ChunksData
        {
            get => chunksData;
            set => chunksData = value;
        }

        public BiomeType[] Biomes => biomes;
        public MapGenerationRules MapGenerationRules => mapGenerationRules;
        public Vector2Int ChunksCount
        {
            get
            {
                switch (mapSize)
                {
                    case MapSize.Test_2km: return new Vector2Int(2, 2);
                    case MapSize.Test_4km: return new Vector2Int(4, 4);
                    case MapSize.Test_8km: return new Vector2Int(8, 8);
                    case MapSize.Test_12km: return new Vector2Int(12, 12);
                    case MapSize.VerySmall_16km: return new Vector2Int(16,16);
                    case MapSize.Small_20km: return new Vector2Int(20, 20);
                    case MapSize.Medium_24km: return new Vector2Int(24, 24);
                    case MapSize.Big_28km: return new Vector2Int(28, 28);
                    case MapSize.VeryBig_32km: return new Vector2Int(32, 32);
                    default:
                        return new Vector2Int(4, 4);
                }
            }
        }
        public Vector2Int WorldSize => new Vector2Int(ChunksCount.x * chunkSize, ChunksCount.y * chunkSize);
        public Vector3Int ChunkSize => new Vector3Int(chunkSize,chunkHeight,chunkSize);
        public bool SmoothTerrain => smoothTerrain;

        public bool FlatShaded => flatShaded;

        public int[,] BiomeMap
        {
            set => biomeMap = value;
        }

        public float[,] HeightMap
        {
            set
            {
                heightMap = value;
                float totalHeight = 0;
                foreach (var mapStep in heightMap)
                    totalHeight += mapStep;
                
                Debug.Log($"WORLD_DATA: Set heightMap data. Total height- {totalHeight}. \n" +
                          $" Average- {totalHeight / heightMap.Length}. \n" +
                          $" SizeX- {heightMap.GetLength(0)}. \n" +
                          $" SizeY- {heightMap.GetLength(1)}");
            }
        }

        public float[,] GetHeightMap(int startX = 0, int startY = 0, int sizeX = -1, int sizeY = -1)
        {
            if (sizeX == -1)
                sizeX = WorldSize.x;
            if (sizeY == -1)
                sizeY = WorldSize.y;
           //Debug.Log("try get height \n " +
                      //$"StartX {startX} StartY {startY} SizeX {sizeX} SizeY {sizeY}");

                      // Debug.Log("WORLD_DATA: Try get heightMap. \n" +
                      //           $"startX- {startX}. \n" +
                      //           $"startY- {startY}. \n" +
                      //           $"sizeX- {sizeX}. \n" +
                      //           $"sizeY- {sizeY}");
                      
            float[,] returnMap = new float[sizeX, sizeY];
            for (int rootX = startX,mapX = 0; mapX < sizeX; rootX++, mapX ++)
            {
                for (int rootY = startY,mapY = 0; mapY < sizeY; rootY++,mapY++)
                {
                    returnMap[mapX, mapY] = heightMap[rootX, rootY];
                }
            }
            
            
            // float totalHeight = 0;
            // foreach (var mapStep in returnMap)
            //     totalHeight += mapStep;
            //
            // Debug.Log($"WORLD_DATA: Return TotalHeight of map {totalHeight}. \n" +
            //           $"Average {totalHeight / returnMap.Length}. \n" +
            //           $"Lenght- {returnMap.Length}");

            return returnMap;
        }
        public float[,] GetBiomeMap(int startX = 0, int startY = 0, int sizeX = -1, int sizeY = -1)
        {
            if (sizeX == -1)
                sizeX = WorldSize.x;
            if (sizeY == -1)
                sizeY = WorldSize.y;

            float[,] map = new float[sizeX - startX, sizeY - startY];
            for (int x = startX; x < map.GetLength(0); x++)
            {
                for (int y = startY; y < map.GetLength(1); y++)
                    map[x, y] = biomeMap[x, y];
            }

            return map;
        }
    }
    
}