using System;
using System.Collections.Generic;
using Blocks.Core;
using UnityEngine;

namespace _Core.Scripts.Build
{
    [CreateAssetMenu(fileName = "BlocksData", menuName = "Build/Blocks data", order = 0)]
    public class BlocksHolder : ScriptableObject
    {
        [SerializeField] private GameObject[] blocks;
        public GameObject[] AllBlocks => blocks;

        private Dictionary<string, GameObject> blocksMap;
        public Dictionary<string, GameObject> BlockMap
        {
            get
            {
                if (blocksMap == null)
                {
                    blocksMap = new Dictionary<string, GameObject>();
                    foreach (var block in AllBlocks)
                    {
                        var obj = block;
                        var id = obj.GetComponent<BaseBlock>().ID;
                        blocksMap.Add(id, obj);
                    }
                }

                return blocksMap;
            }
        }

        private void OnValidate()
        {
            blocksMap = null;
        }
    }
}