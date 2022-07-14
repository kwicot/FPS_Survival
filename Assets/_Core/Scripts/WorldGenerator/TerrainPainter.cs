using System;
using System.Collections;
using System.Collections.Generic;
using _Core.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace _Core.Scripts.WorldGeneratorCore
{
    public class TerrainPainter : IGenerator
    {
        [Serializable]
        public class SplatHeights
        {
            public int textureIndex;
            public int startHeight;
        }

        [SerializeField] private SplatHeights[] splatHeights;
        [SerializeField] private bool wait;
        [SerializeField] private float waitTime;

        IEnumerator Paint(UnityAction OnEnd)
        {
            yield return null;
            OnEnd?.Invoke();
        }

        float[,,] SetTexture(float[,,] splatData, int x, int y, int layer)
        {
            for (int i = 0; i < splatData.GetLength(2); i++)
            {
                if (i == layer)
                    splatData[x, y, i] = 1;
                else
                    splatData[x, y, i] = 0;
            }

            return splatData;
        }

        public override void Execute(object data, UnityAction OnEnd)
        {
            StartCoroutine(Paint(OnEnd));
        }
    }
}
