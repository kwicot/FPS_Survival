using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace _Core.Scripts.WorldGeneratorCore
{
    public class ChunksGenerator : IGenerator
    {

        [SerializeField] private GameObject chunkPrefab;

        private Transform chunksParent;

        private Chunk[,] chunksData;
        private Vector3Int chunkSize => worldGenerator.WorldData.ChunkSize;

        private Vector2Int ChunksCount => worldGenerator.WorldData.ChunksCount;


        public override void Execute(object data, UnityAction OnEnd)
        {
            StartCoroutine(Generate(OnEnd));
        }

        IEnumerator Generate(UnityAction OnEnd)
        {
            yield return GenerateChunks();
            worldGenerator.WorldData.ChunksData = chunksData;
            yield return null;
            OnEnd?.Invoke();
        }

        IEnumerator GenerateChunks()
        {
            var startX = ChunksCount.x / 2 * -1;
            var startY = ChunksCount.y / 2 * -1;
            var endX = ChunksCount.x / 2;
            var endY = ChunksCount.y / 2;

            chunksData = new Chunk[ChunksCount.x, ChunksCount.y];

            for (int stepX = startX, x = 0; stepX < endX; stepX++, x++)
            {
                for (int stepY = startY, y = 0; stepY < endY; stepY++, y++)
                {
                    var chunkObject = Instantiate(chunkPrefab);
                    chunkObject.name = $"Chunk_{x}_{y}";
                    var chunk = chunkObject.AddComponent<Chunk>();

                    int positionX = chunkSize.x * stepX;
                    int positionZ = chunkSize.z * stepY;
                    Vector3 position = new Vector3(positionX, 0, positionZ);


                    chunkObject.transform.position = position;
                    chunkObject.transform.SetParent(GetChunksParent(), true);
                    Vector2Int id = new Vector2Int(x, y);

                    float[,] heightMap =
                        worldGenerator.WorldData.GetHeightMap(chunkSize.x * x, chunkSize.z * y, chunkSize.x+1, chunkSize.z+1);
                    
                    // float totalHeight = 0;
                    // foreach (var mapStep in heightMap)
                    //     totalHeight += mapStep;
                    //
                    // Debug.Log($"CHUNK_GENERATOR: TotalHeight of map {totalHeight}, average {totalHeight / heightMap.Length}");
                    
                    yield return chunk.Init(worldGenerator.WorldData, id, heightMap);
                    chunksData[x, y] = chunk;
                    yield return new WaitForFixedUpdate();
                }
            }
        }


        private Transform GetChunksParent()
        {
            if (!chunksParent)
            {
                chunksParent = new GameObject("Chunks").transform;
                chunksParent.transform.position = Vector3.zero;
                chunksParent.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            return chunksParent;
        }

    }
}
