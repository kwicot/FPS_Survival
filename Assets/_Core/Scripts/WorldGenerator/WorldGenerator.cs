using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using _Core.Scripts.WorldGeneratorCore.Rules;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace _Core.Scripts.WorldGeneratorCore
{
    public class WorldGenerator : MonoBehaviour
    {
        [SerializeField] private WorldData worldData;
        [SerializeField] private List<IGenerator> executeOrder;
        
        private int executeIndex = 0;

        
        public WorldData WorldData => worldData;


        private void Start()
        {
            Debug.Log("Start");
            foreach (var generator in executeOrder)
                generator.Init(this);
            Debug.Log("Init end");
            StartCoroutine(GenerateMaps(delegate
            {
                Debug.Log("Generate end");
                ExecuteNext(executeIndex++);
            }));
        }
        

        void ExecuteNext(int index)
        {
            Debug.Log($"Execute index {index} order count {executeOrder.Count}");
            if(index < executeOrder.Count)
                executeOrder[index].Execute(null,delegate { ExecuteNext(executeIndex++); });
        }

        IEnumerator GenerateMaps(UnityAction OnEnd)
        {
            Debug.Log("Start generate heightmap");
            yield return GenerateHeightMap();
            yield return GenerateBiomeMap();
            //CreateTexture();
            OnEnd?.Invoke();
        }

        void CreateTexture()
        {
            var texture = new Texture2D(worldData.WorldSize.x, worldData.WorldSize.y);
            var heightMap = worldData.GetHeightMap();
            Color[] colors = new Color[worldData.WorldSize.x * worldData.WorldSize.y];
            for (int x = 0; x < worldData.WorldSize.x; x++)
            {
                for (int y = 0; y < worldData.WorldSize.y; y++)
                {
                    var value = heightMap[y, x];
                    colors[x + worldData.WorldSize.y * y] = new Color(value, value, value, 1);
                }
            }
            texture.SetPixels(colors);
            texture.Apply();
            var bytes = texture.EncodeToPNG();
            var path = Path.Combine(Application.dataPath, "TestTexture2D.png");
            if(File.Exists(path))
                File.Delete(path);
            
            
            File.WriteAllBytes(path,bytes);
        }

        IEnumerator GenerateHeightMap(UnityAction OnEnd = null)
        {
            int width = worldData.WorldSize.x;
            int height = worldData.WorldSize.y;
            int seed = worldData.MapGenerationRules.Seed;
            int octaves = worldData.MapGenerationRules.Octaves;
            float noiseScale = worldData.MapGenerationRules.NoiseScale;
            float persistance = worldData.MapGenerationRules.Persistence;
            float lacunarity = worldData.MapGenerationRules.Lacunarity;
            Vector2 offset = worldData.MapGenerationRules.Offset;

            
            float[,] heightMap = new float[width + worldData.ChunksCount.x, height + worldData.ChunksCount.y];
            if (seed == PersistentCache.TryLoad<int>("WorldSeed"))
            {
                heightMap = PersistentCache.TryLoad<float[,]>("HeightMap");
                if (heightMap != null)
                {
                    Debug.Log("WORLD_GENERATOR: Loaded seed");
                    worldData.HeightMap = heightMap;
                    yield break;
                }
            }

            int lastX = 0;
            int lastY = 0;
            for (int x = 0; x < width; x+=worldData.ChunkSize.x+1)
            {
                for (int y = 0; y < height; y+=worldData.ChunkSize.z + 1)
                {
                    offset = new Vector2(y+worldData.MapGenerationRules.Offset.y, x+worldData.MapGenerationRules.Offset.x);
                    
                    var chunkHeightMap= Noise.GenerateNoiseMap(worldData.ChunkSize.x + 1,worldData.ChunkSize.z + 1, seed, noiseScale, octaves, persistance, lacunarity, offset);

                    for (int chunkX = 0,mapX = x; chunkX < chunkHeightMap.GetLength(0); chunkX++,mapX++)
                    {
                        for (int chunkY = 0,mapY = y; chunkY < chunkHeightMap.GetLength(1); chunkY++,mapY++)
                        {
                            heightMap[mapX, mapY] = chunkHeightMap[chunkY, chunkX];
                        }
                    }
                    
                    
                    // for (int pieceX = 0,mapX = lastX; pieceX < piece.GetLength(0); pieceX++,mapX ++)
                    // {
                    //     for (int pieceY = 0, mapY = lastY; pieceY < piece.GetLength(1); pieceY++, mapY++)
                    //     {
                    //         noiseMap[mapX, mapY] = piece[pieceX, pieceY];
                    //     }
                    // }
                    
                    

                    float totalHeight = 0;
                    foreach (var mapStep in chunkHeightMap)
                        totalHeight += mapStep;

                    //Debug.Log($"WORLD_GENERATOR: TotalHeight of map {totalHeight}, average {totalHeight / chunkHeightMap.Length}");
                    
                    lastX = x;
                    lastY = y;
                    //Debug.Log($"Generated worldChunk {heightMapPeace.Length}");
                yield return null;
                }
            }
            
            
            WorldData.HeightMap = heightMap;
            PersistentCache.Save("Seed", seed);
            PersistentCache.Save("HeightMap", heightMap);
            yield return null;
        }
        IEnumerator GenerateBiomeMap(UnityAction OnEnd = null)
        {
            int width = worldData.WorldSize.x;
            int height = worldData.WorldSize.y;
            int seed = worldData.MapGenerationRules.Seed;
            int biomeGrid = worldData.MapGenerationRules.BiomeGrid;
            BiomeType[] biomes = worldData.Biomes;
            float noiseDist = worldData.MapGenerationRules.NoiseDist;
            float noiseMult = worldData.MapGenerationRules.NoiseMult;
            
            int[,] biomeMap = Noise.GenerateBiomeMap(width, height, seed, biomeGrid, biomes.Length, noiseMult, noiseDist);
            worldData.BiomeMap = biomeMap;
            yield return null;
        }

        
        
        
        


    }
}