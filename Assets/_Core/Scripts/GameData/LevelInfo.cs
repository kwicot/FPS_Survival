using System;
using UnityEngine;

namespace _Core.Scripts
{
    [Serializable]
    public class LevelInfo
    {
        [SerializeField] private string name;
        [SerializeField] private string sceneName;
        [SerializeField] private Sprite preview;
        [SerializeField] private string size;

        public string Name => name;
        public Sprite Preview => preview;
        public string Size => size;
        public string SceneName => sceneName;
    }
}