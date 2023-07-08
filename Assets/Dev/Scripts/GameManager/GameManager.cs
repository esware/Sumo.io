using System;
using Dev.Scripts;
using UnityEngine;

public struct GameEvents
{
    public static Action<GameObject> onSugarConsumed = null;
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnArea;
    [SerializeField] private int numberOfEnemies = 5;
    [SerializeField] private int numberOfCandies=10;

    private ObjectSpawner _objectSpawner;
    public CandyPool candyPool;

    private void Start()
    {
        _objectSpawner = new ObjectSpawner();
        StartGame();
    }

    private void StartGame()
    {
        SpawnEnemies();
        SpawnCandies();
    }

    private void OnEnable()
    {
        SignUpEvents();
    }

    private void SignUpEvents()
    {
        GameEvents.onSugarConsumed += OnSugarConsumed;
    }
    
    private void OnSugarConsumed(GameObject obj)
    {
        GameObject candy = candyPool.GetCandyFromPool();
        candy.transform.position = _objectSpawner.GetRandomSpawnPosition(spawnArea);
    }
    private void SpawnEnemies()
    {
        _objectSpawner.SpawnObjects(enemyPrefab, numberOfEnemies, spawnArea);
    }

    private void SpawnCandies()
    {
        for (int i = 0; i < numberOfCandies; i++)
        {
            GameObject candy = candyPool.GetCandyFromPool();
            candy.transform.position = _objectSpawner.GetRandomSpawnPosition(spawnArea);
        }
    }
}


