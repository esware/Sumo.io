using System;
using System.Collections.Generic;
using Dev.Scripts;
using Managers;
using ScriptableObjects.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class ObjectSpawner:MonoBehaviour
{
    public readonly List<Vector3> spawnedPositions;
    public List<GameObject> enemyList;

    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject enemyPrefab; 
    
    
    [Header("Level Data")]
    private int _numberOfEnemies; 
    private int _numberOfCandies;
    private Transform _spawnArea;
    
    private ObjectSpawner _objectSpawner;
    private CandyPool _candyPool;
    
    private void Start()
    {
        SignUpEvents();
        SpawnGameElements();
    }
    
    private void SignUpEvents()
    {
        GameEvents.collectableEvent += OnSugarConsumed;
        GameEvents.enemyDeathEvent += ResizeList;
    }

    private void SpawnGameElements()
    {
        _candyPool = GetComponent<CandyPool>();
        AssingLevelData();
        SpawnCandies();
        SpawnEnemies();
    }

    private void AssingLevelData()
    {
        _spawnArea = levelManager.LevelData.levelObject.transform.GetChild(0).transform;
        _numberOfCandies = levelManager.LevelData.numberOfCandies;
        _numberOfEnemies = levelManager.LevelData.numberOfEnemies;
    }
    
    private void SpawnCandies()
    {
        for (int i = 0; i < _numberOfCandies; i++)
        {
            var candy = _candyPool.GetCandyFromPool();
            candy.transform.position = GetRandomSpawnPosition(_spawnArea);
        }
    }
    
    private void SpawnEnemies()
    {
        enemyList.Clear();
        enemyList = SpawnObjects(enemyPrefab,_numberOfEnemies, _spawnArea);
    }
    
    void ResizeList(GameObject enemy)
    {
        enemyList.Remove(enemy);
        
    }
    
    private void OnSugarConsumed(GameObject obj)
    {
        var candy = _candyPool.GetCandyFromPool();
        candy.transform.position = GetRandomSpawnPosition(_spawnArea);
    }

    public ObjectSpawner()
    {
        spawnedPositions = new List<Vector3>();
    }

    public List<GameObject> SpawnObjects(GameObject objectPrefab, int numObjects,Transform spawnArea)
    {
        List<GameObject> spawnedObjects = new List<GameObject>();
        
        for (int i = 0; i < numObjects; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition(spawnArea);
            GameObject spawnedObj = Object.Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            spawnedPositions.Add(spawnPosition);
            spawnedObjects.Add(spawnedObj);
        }

        return spawnedObjects;
    }

    private bool CheckValidSpawnPosition(Vector3 position)
    {
        for (int i = 0; i < spawnedPositions.Count; i++)
        {
            float distance = Vector3.Distance(position, spawnedPositions[i]);
            if (distance < 5f)
            {
                return false;
            }
        }

        return true;
    }

    private Vector3 GetRandomSpawnPosition(Transform spawnArea)
    {
        var randomPoint = Vector3.zero;
        var spawnAreaPosition = spawnArea.position;
        var spawnAreaSize = spawnArea.localScale;
        
        for (int i = 0; i < 500; i++)
        {
            randomPoint = new Vector3(
                Random.Range(spawnAreaPosition.x - spawnAreaSize.x / 2f, spawnAreaPosition.x + spawnAreaSize.x / 2f),
                spawnAreaPosition.y + 3.5f,
                Random.Range(spawnAreaPosition.z - spawnAreaSize.z / 2f, spawnAreaPosition.z + spawnAreaSize.z / 2f)
            );

            var isValidSpawnPosition = CheckValidSpawnPosition(randomPoint);

            if (isValidSpawnPosition)
            {
                return randomPoint;
            }
        }
        return Vector3.zero;
    }

}
