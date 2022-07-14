using System.Collections;
using _Core.Scripts.WorldGeneratorCore;
using UnityEngine;

namespace _Core.Scripts
{
    public class ChunksManager : MonoBehaviour
    {
        [SerializeField] private WorldGenerator worldGenerator;
        [SerializeField] private Transform player;
        [SerializeField] private float updateInterval = 1f;
        [SerializeField] private int chunksLoaded = 3;

        private void Start()
        {
            StartCoroutine(UpdateChunksActivity());
        }

        IEnumerator UpdateChunksActivity()
        {
            while (true)
            {
                if (worldGenerator.WorldData.ChunksData != null)
                {
                    Chunk[,] chunksData = worldGenerator.WorldData.ChunksData;
                    Vector2Int playerPosition = GetPlayerPosition();
                    Debug.Log($"Player position {playerPosition}");
                    
                    for (int x = 0; x < chunksData.GetLength(0); x++)
                    {
                        for (int y = 0; y < chunksData.GetLength(1); y++)
                        {
                            var id = chunksData[x, y].ID;
                            
                            if (id.x >= +playerPosition.x - 1 && id.x <= playerPosition.x + 1 &&
                                id.y >= playerPosition.y - 1 && id.y <= playerPosition.y + 1)
                            {
                                //InRange
                                chunksData[x, y].MeshRenderer.enabled = true;
                            }
                            else
                            {
                                //OutOfRange
                                chunksData[x, y].MeshRenderer.enabled = false;

                            }

                            yield return null;

                        }
                    }
                }
                yield return new WaitForSecondsRealtime(updateInterval);
            }
        }

        Vector2Int GetPlayerPosition()
        {
            Ray ray = new Ray();
            ray.origin = player.position + Vector3.up * 1000;
            ray.direction = Vector3.down;
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (var hit in hits)
            {
                if (hit.transform.TryGetComponent(out Chunk chunk))
                    return chunk.ID;
            }
            return new Vector2Int(0,0);
        }
    }
}