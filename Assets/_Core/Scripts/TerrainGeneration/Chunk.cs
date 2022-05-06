using System;
using UnityEngine;

namespace _Core.Scripts.TerrainGeneration
{
    public class Chunk : MonoBehaviour
    {
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        

        public Vector3 Position => transform.position;
        public MeshFilter MeshFilter => meshFilter;
        public MeshRenderer MeshRenderer => meshRenderer;
        
        public Mesh Mesh => meshFilter.mesh;

        private void Awake()
        {
            meshFilter = gameObject.AddComponent<MeshFilter>();
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }
    }
}