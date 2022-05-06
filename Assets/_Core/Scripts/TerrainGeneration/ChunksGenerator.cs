using System;
using System.Collections;
using UnityEngine;

namespace _Core.Scripts.TerrainGeneration
{
    public class ChunksGenerator : MonoBehaviour
    {
        [SerializeField] private Vector2Int chunksCount;
        [SerializeField] private float chunkSize;

        private Chunk[,] chunks;
        
        public void Generate()
        {
            ClearChunksData();
            GenerateChunks(chunksCount,chunkSize);
        }

        void GenerateChunks(Vector2Int count,float size)
        {
            chunks = new Chunk[count.x, count.y];
            
            for (int x = 0; x < count.x; x++)
            {
                for (int y = 0; y < count.y; y++)
                {
                    chunks[x, y] = CreateChunk(x,y, size);
                }
            }                        
        }

        Chunk CreateChunk(float x, float y, float size)
        {
            var obj = new GameObject($"Chunk_{x}_{y}");
            obj.transform.position = new Vector3(x * size, 0, y * size);
            var chunk = obj.AddComponent<Chunk>();
            return chunk;
        }

        void ClearChunksData()
        {
            if(chunks == null) return;

            foreach (var chunk in chunks)
                Destroy(chunk.gameObject);
            
            chunks = null;
        }

        private void OnValidate()
        {
            Generate();
        }
    }
}