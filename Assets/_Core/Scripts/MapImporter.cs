using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace _Core.Scripts
{
    public class MapImporter : MonoBehaviour
    {
        [SerializeField] private Texture2D heightMapTexture;

        private float[,] heightMap;

        private TerrainData[,] terrainsData;
        private GameObject[,] terrains;
        private int dataSize = 8;
        int size = 1024;


        private void Start()
        {
            StartCoroutine(GenerateMap());
        }

        IEnumerator GenerateMap()
        {
            yield return ReadTexture();
            Debug.Log("Read texture done");
            yield return CreateTerrains();
            Debug.Log("Create terrains done");
            yield return ReSize();
            Debug.Log("Resize done");
            yield return Move();
            Debug.Log("Move done");
            yield return SaveTerrains();
            Debug.Log("Save done");
        }
        
        private IEnumerator ReadTexture()
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
        private IEnumerator CreateTerrains()
        {
            terrainsData = new TerrainData[dataSize,dataSize];
            terrains = new GameObject[dataSize, dataSize];
            Debug.Log("Start create terrains");
            
            for (int i = 0; i < dataSize; i++)
            {
                for (int j = 0; j < dataSize; j++)
                {
                    var map = new float[size, size];
                    
                    for (int x = 0; x < size; x++)
                        for (int y = 0; y < size; y++)
                            map[x, y] = heightMap[y + i * size, x + j * size];
                    
                    var terrainData = new TerrainData();
                    terrainData.heightmapResolution = size+1;
                    terrainData.size = new Vector3(size, size / 4, size);
                    terrainData.SetHeights(0,0,map);
                    var terrain = Terrain.CreateTerrainGameObject(terrainData);
                    terrain.transform.position = new Vector3(i * size, 100, j * size);
                    
                    terrainsData[i, j] = terrainData;
                    terrains[i, j] = terrain;
                    
                    yield return null;
                }
            }

            Debug.Log("Create terrains done");
        }
        private IEnumerator ReSize()
        {
            for (int x = 0; x < dataSize; x++)
            {
                for (int y = 0; y < dataSize; y++)
                {
                    var terrainData = terrainsData[x, y];
            
                    terrainData.size = new Vector3(size * 4, size, size * 4);
                    yield return null;
                }
            }

            Debug.Log("End");
        }
        private IEnumerator Move()
        {
            for (int x = 0; x < dataSize; x++)
            {
                for (int y = 0; y < dataSize; y++)
                {
                    var data = terrains[x,y];
                    var pos = data.transform.position;
                    pos.x *= 4;
                    pos.z *= 4;

                    data.transform.position = pos;
                    yield return null;
                }
            }
        }
        private IEnumerator SaveTerrains()
        {
            for (int x = 0; x < dataSize; x++)
            {
                for (int y = 0; y < dataSize; y++)
                {
                    AssetDatabase.CreateAsset(terrainsData[x,y],$"Assets/TerrainData/Terrain_{x}_{y}.asset");
                    
                }

                yield return null;
            }
        }

    }
}