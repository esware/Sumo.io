using UnityEngine;

namespace ScriptableObjects.Scripts
{
    [CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        public LevelData[] levelsDataArray;

        private int _lastLevelIndex = 0;

        public int LastLevelIndex
        {
            get => _lastLevelIndex;
            set => _lastLevelIndex = value;
        }
    }
}