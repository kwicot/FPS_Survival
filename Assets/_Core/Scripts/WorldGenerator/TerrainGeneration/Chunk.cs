using System;
using System.Collections;
using System.Collections.Generic;
using _Core.Scripts.MarchingCubesCore;
using _Core.Scripts.WorldGeneratorCore.Marching;
using UnityEngine;

namespace _Core.Scripts.WorldGeneratorCore
{
    public class Chunk : MonoBehaviour
    {
        #region MainLogic

        

        [SerializeField] private Vector2Int id;
        [SerializeField] private Vector3Int size;
        [SerializeField] private WorldData worldData; 
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private MeshCollider meshCollider;
        private float[,] heightMap;

        //
        //
        //
        private List<Vector3> vertices = new List<Vector3>();
        private List<int> triangles = new List<int>();
        
        private int configIndex;
        
        float terrainSurface = 0.5f;
        private float[,,] terrainMap;
        
        //
        //
        //

        public MeshRenderer MeshRenderer => meshRenderer;
        public Vector3 Position => transform.position;
        public Vector2Int ID => id;
        
        private IEnumerator InitializeTerrain()
        {
            yield return null;
            GetComponents();
            //Инициализация поверхности и коллайдера поверхности
            // terrain = TryGetComponent(out Terrain _terrain)
            //     ? _terrain
            //     : throw new Exception($"Cant get terrain on {gameObject.name}");
            // terrainCollider = GetComponent<TerrainCollider>();
            //
            // terrain.terrainData = new TerrainData();
            // terrainCollider.terrainData = TerrainData;
            //
            // TerrainData.name = gameObject.name;
            // TerrainData.heightmapResolution = size.x + 1;
            // TerrainData.size = size;
            // TerrainData.alphamapResolution = size.x;

            ////

            terrainMap = new float[size.x + 1, size.y + 1, size.z + 1];
            transform.tag = "Terrain";
            PopulateTerrainMap();
            CreateMeshData();
            BuildMesh();
            
            // yield return MakeGrid();
            // yield return Noise2d();
            // March();
        }

        void GetComponents()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
            meshCollider = GetComponent<MeshCollider>();
        }
        #endregion

        #region MarchingCubes_7DaysToDie
        
        
        
        void CreateMeshData() {

            ClearMeshData();

            // Loop through each "cube" in our terrain.
            for (int x = 0; x < size.x; x++) {
                for (int y = 0; y < size.y; y++) {
                    for (int z = 0; z < size.z; z++) {

                        // Create an array of floats representing each corner of a cube and get the value from our terrainMap.
                        float[] cube = new float[8];
                        for (int i = 0; i < 8; i++) {

                            Vector3Int corner = new Vector3Int(x, y, z) + MarchingCubes.CornerTable[i];
                            cube[i] = terrainMap[corner.x, corner.y, corner.z];
                        }

                        // Pass the value into our MarchCube function.
                        MarchCube(new Vector3(x, y, z), cube);

                    }
                }
            }

            BuildMesh();

        }
        int GetCubeConfig(float[] cube)
        {
            int configurationIndex = 0;
        
            for (int i = 0; i < 8; i++)
            {
                if (cube[i] > terrainSurface)
                    configurationIndex |= 1 << i;
            }
        
            return configurationIndex;
        }
        void MarchCube (Vector3 position, float[] cube) {

            // Get the configuration index of this cube.
            int configIndex = GetCubeConfig(cube);

            // If the configuration of this cube is 0 or 255 (completely inside the terrain or completely outside of it) we don't need to do anything.
            if (configIndex == 0 || configIndex == 255)
                return;

            // Loop through the triangles. There are never more than 5 triangles to a cube and only three vertices to a triangle.
            int edgeIndex = 0;
            for(int i = 0; i < 5; i++) {
                for(int p = 0; p < 3; p++) {

                    // Get the current indice. We increment triangleIndex through each loop.
                    int indice = MarchingCubes.TriangleTable[configIndex, edgeIndex];

                    // If the current edgeIndex is -1, there are no more indices and we can exit the function.
                    if (indice == -1)
                        return;

                    // Get the vertices for the start and end of this edge.
                    Vector3 vert1 = position + MarchingCubes.EdgeTable[indice, 0];
                    Vector3 vert2 = position + MarchingCubes.EdgeTable[indice, 1];

                    // Get the midpoint of this edge.
                    Vector3 vertPosition;
                    if (worldData.SmoothTerrain) {

                        // Get the terrain values at either end of our current edge from the cube array created above.
                        float vert1Sample = cube[MarchingCubes.EdgeIndexes[indice, 0]];
                        float vert2Sample = cube[MarchingCubes.EdgeIndexes[indice, 1]];

                        // Calculate the difference between the terrain values.
                        float difference = vert2Sample - vert1Sample;

                        // If the difference is 0, then the terrain passes through the middle.
                        if (difference == 0)
                            difference = terrainSurface;
                        else
                            difference = (terrainSurface - vert1Sample) / difference;

                        // Calculate the point along the edge that passes through.
                        vertPosition = vert1 + ((vert2 - vert1) * difference);


                    } else {

                        // Get the midpoint of this edge.
                        vertPosition = (vert1 + vert2) / 2f;

                    }
                    
                    vertices.Add(vertPosition);
                    triangles.Add(vertices.Count - 1);
                    edgeIndex++;

                }
            }
        }

        float SampleTerrain(Vector3Int point)
        {
            return terrainMap[point.x, point.y, point.z];
        }

        int VertForIndice(Vector3 vert)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i] == vert)
                    return i;
            }
            vertices.Add(vert);
            return vertices.Count - 1;
        }
        void PopulateTerrainMap () {

            // The data points for terrain are stored at the corners of our "cubes", so the terrainMap needs to be 1 larger
            // than the width/height of our mesh.
            for (int x = 0; x < size.x + 1; x++) {
                for (int z = 0; z < size.z + 1; z++) {
                    for (int y = 0; y < size.y + 1; y++) {

                        // Get a terrain height using regular old Perlin noise.
                        float thisHeight = heightMap[x, z] * size.y;

                        terrainMap[x, y, z] = (float)y - thisHeight;
                        
                    }
                }
            }
        }

        public void PlaceTerrain(Vector3 pos)
        {
            Vector3Int v3Int = new Vector3Int(Mathf.CeilToInt(pos.x), Mathf.CeilToInt(pos.y), Mathf.CeilToInt(pos.z));
            terrainMap[v3Int.x, v3Int.y, v3Int.z] = 0f;
            CreateMeshData();
        }

        public void RemoveTerrain(Vector3 pos)
        {
            Vector3Int v3Int = new Vector3Int(Mathf.FloorToInt(pos.x), Mathf.CeilToInt(pos.y), Mathf.CeilToInt(pos.z));
            terrainMap[v3Int.x, v3Int.y, v3Int.z] = 1f;
            CreateMeshData();
        }
        void ClearMeshData()
        {
            vertices.Clear();
            triangles.Clear();
        }
        void BuildMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            
            
            mesh.RecalculateNormals();
            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;
            meshCollider.convex = false;
        }
        private void InitializeHeights()
        {
            float totalHeight = 0;
            foreach (var mapStep in heightMap)
                totalHeight += mapStep;
        
            Debug.Log($"CHUNK: TotalHeight of map {totalHeight}, average {totalHeight / heightMap.Length}");
            //TerrainData.SetHeights(0,0,heightMap);
        }
        
        #endregion


        public IEnumerator Init(WorldData data, Vector2Int id, float[,] heightMap)
        {
            this.id = id;
            this.heightMap = heightMap;
            this.worldData = data;
            this.size = worldData.ChunkSize;
            StartCoroutine(InitializeTerrain());
            //yield return InitializeHeights();
            // PainTerrain();
            // GenerateTrees();
            yield return null;
        }
    }
}