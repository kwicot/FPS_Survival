using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainConfigure : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Configure());
    }

    private IEnumerator Configure()
    {
        for (int x = 0; x < 32; x++)
        {
            for (int y = 0; y < 32; y++)
            {
                var data = transform.GetChild(x + (y * 32));
                var terrainData = data.GetComponent<Terrain>().terrainData;

                terrainData.heightmapResolution = 641;
                yield return null;
            }
        }
    }
}
