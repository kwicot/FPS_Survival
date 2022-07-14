using UnityEngine;

namespace _Core.Scripts
{
    [CreateAssetMenu(fileName = "NewGameData", menuName = "Game/Data", order = 0)]
    public class GameData : ScriptableObject
    {
        [SerializeField] private LevelInfo[] levelsInfo;

        
        public LevelInfo[] LevelsInfo => levelsInfo;
    }
}