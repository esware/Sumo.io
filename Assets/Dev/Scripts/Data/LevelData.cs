using Unity.VisualScripting;
using UnityEngine;

namespace ScriptableObjects.Scripts
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
    public class LevelData : ScriptableObject
    {
        [Header("Level")] [SerializeField] public GameObject levelObject;
        [SerializeField]
        public float levelCompleteTime;
        
        [Space,Header("Enter the number of enemies that will be spawn")]
        [SerializeField]
        public int numberOfEnemies;
        
        [Space,Header("Enter the number of candies that will be spawn")]
        [SerializeField]
        public int numberOfCandies;
    }
}