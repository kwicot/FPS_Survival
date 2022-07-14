using System;
using Unity.Mathematics;
using UnityEngine;

namespace Blocks.Core
{
    [Serializable]
    public class BlockModel
    {
        public float3 position { get; set; }
        public float3 rotation { get; set; }
        public string id { get; set; }
    }
}