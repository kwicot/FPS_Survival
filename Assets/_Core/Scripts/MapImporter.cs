using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

namespace _Core.Scripts
{
    public class MapImporter : MonoBehaviour
    {
        [SerializeField] private Texture2D heightMapTexture;

        private float[,] heightMap;

        private void Start()
        {
            StartCoroutine(GenerateMap());
        }

        IEnumerator GenerateMap()
        {
            yield return ReadTexture();
            yield return CreateTerrains();

        }

        IEnumerator CreateTerrains()
        {
            Debug.Log("Start create terrains");
            int size = 1024;
            
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var map = new float[size, size];
                    
                    for (int x = 0; x < size; x++)
                        for (int y = 0; y < size; y++)
                            map[x, y] = heightMap[y + i * size, x + j * 1024];
                    
                    var terrainData = new TerrainData();
                    terrainData.heightmapResolution = size + 1;
                    terrainData.size = new Vector3(size, size / 4, size);
                    terrainData.SetHeights(0,0,map);
                    var terrain = Terrain.CreateTerrainGameObject(terrainData);
                    terrain.transform.position = new Vector3(i * size, 100, j * size);
                    yield return null;
                }
            }

            Debug.Log("Create terrains done");
        }

        IEnumerator ReadTexture()
        {
            Debug.Log("Start read texture");
            heightMap = new float[heightMapTexture.width,heightMapTexture.height];
            Debug.Log(heightMapTexture.width + " " + heightMapTexture.height);

            int waitCycle = 100;
            for (int x = 0; x < heightMapTexture.width; x++)
            {
                for (int y = 0; y < heightMapTexture.height; y++)
                {
                    var valueColor = heightMapTexture.GetPixel(x, y);
                    var height = (valueColor.r + valueColor.g + valueColor.b) / 3;

                    


                    heightMap[x, y] = height;
                }

                if (x % waitCycle == 0) yield return null;
            }
            Debug.Log("Read texture done");
        }
    }
}