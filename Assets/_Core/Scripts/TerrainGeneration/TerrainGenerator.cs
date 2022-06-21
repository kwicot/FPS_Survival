using System;
using _Core.Scripts.TerrainGeneration;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField]private Terrain TerrainMain;
    [SerializeField] private HeightMapGenerator generator;
    [SerializeField] private int size = 256;
    [SerializeField] private int depth = 20;
    [SerializeField] private int terrainPower;
    [SerializeField] private float outsideHeight;
    [SerializeField] private float roughness;
    [SerializeField] private bool raiseToSecondPower;
    [SerializeField] private bool normalize = true;
    [SerializeField] private float baseHeight;
    [SerializeField] private int smoothCycles;
    
    //public PaintTerrain paintTerrain;

    private void Start()
    {
        generator.Init(terrainPower,outsideHeight,roughness,raiseToSecondPower,normalize,baseHeight,smoothCycles);
        generator.Generate(delegate(float[,] map)
        {
            Debug.Log("End");
            size = (int)Mathf.Pow(2, terrainPower) + 1;
            TerrainMain.terrainData.heightmapResolution = size + 1;
            TerrainMain.terrainData.size = new Vector3(size,depth,size);
            TerrainMain.terrainData.SetHeights(0, 0, map);
        }, delegate(float arg0)
        {
            Debug.Log($"Generation progress {arg0}");
        } );
        

    }

    void OnValidate()
    {
        //paintTerrain.StartPaint();

        // var waterObj = GameObject.Find("Water");
        // waterObj.transform.position = new Vector3(waterObj.transform.position.x, waterLevel * TerrainMain.terrainData.size.y, waterObj.transform.position.z);
    }
}
