using System;
using UnityEngine;

namespace _Core.Scripts.TerrainDigging
{
    public class GigTest : MonoBehaviour
    {
        [SerializeField] private Terrain targetTerrain;
        [SerializeField] private Texture2D craterTexture;

        private int xRes;
        private int yRes;
        private float[,] saved;
        private Color[] craterData;

        private TerrainData TerrainData => targetTerrain.terrainData;

        private void Start()
        {
            xRes = TerrainData.heightmapResolution;
            yRes = TerrainData.heightmapResolution;
            saved = TerrainData.GetHeights(0, 0, xRes, yRes);
            craterData = craterTexture.GetPixels();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButton(0))
            {
                
                
                
                // var terrainLocalPos = pos - targetTerrain.transform.position;
                // var normalizedPos = new Vector2(Mathf.InverseLerp(0.0f, targetTerrain.terrainData.size.x, terrainLocalPos.x),
                //     Mathf.InverseLerp(0.0f, targetTerrain.terrainData.size.z, terrainLocalPos.z));
                // var terrainNormal = targetTerrain.terrainData.GetInterpolatedNormal(normalizedPos.x, normalizedPos.y);
                //
                //
                // Debug.Log (terrainNormal + " " + normalizedPos.x + " " + normalizedPos.y);
                // var sampleHeight = targetTerrain.SampleHeight(pos);
                // var height = TerrainData.GetHeights((int)normalizedPos.x, (int)normalizedPos.y, 1, 1);
                // Debug.Log($"Sample {sampleHeight}, height {height}");

            }  
        }

        void SetHeight(Vector3 worldPosition,int size, float height)
        {
            var index = ConvertWordCor2TerrCor(worldPosition);

            int xBase = (int) index.x;
            int zBase = (int) index.z;
            if (size > 1)
            {
                xBase = (int) index.x - size/2;
                zBase = (int) index.z - size/2;
            }
                
            var heights = TerrainData.GetHeights(xBase,zBase,size,size);
            for (int x = 0; x < heights.GetLength(0); x++)
            {
                for (int y = 0; y < heights.GetLength(1); y++)
                {
                    heights[x, y] = heights[x, y] - 0.01f;
                }
            }
            TerrainData.SetHeights(xBase,zBase,heights);
        }
        
        private Vector3 ConvertWordCor2TerrCor(Vector3 wordCor)
        {
            Vector3 vecRet = new Vector3();
            Terrain ter = targetTerrain;
            Vector3 terPosition = ter.transform.position;
            vecRet.x = ((wordCor.x - terPosition.x) / ter.terrainData.size.x) * ter.terrainData.heightmapResolution;
            vecRet.z = ((wordCor.z - terPosition.z) / ter.terrainData.size.z) * ter.terrainData.heightmapResolution;
            return vecRet;
        }

        Vector3 GetMousePosition()
        {
            var ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000))
            {
                return hit.point;
            }
            return Vector3.zero;
        }
    }
}