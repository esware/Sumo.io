using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner
{
    public List<Vector3> _spawnedPositions;

    public ObjectSpawner()
    {
        _spawnedPositions = new System.Collections.Generic.List<Vector3>();
    }

    public void SpawnObjects(GameObject objectPrefab, int numObjects,Transform spawnArea)
    {
        for (int i = 0; i < numObjects; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition(spawnArea);
            Object.Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            _spawnedPositions.Add(spawnPosition);
        }
    }

    private bool CheckValidSpawnPosition(Vector3 position)
    {
        for (int i = 0; i < _spawnedPositions.Count; i++)
        {
            float distance = Vector3.Distance(position, _spawnedPositions[i]);
            if (distance < 5f)
            {
                return false;
            }
        }

        return true;
    }

    public Vector3 GetRandomSpawnPosition(Transform spawnArea)
    {
        Vector3 randomPoint = Vector3.zero;
        var spawnAreaPosition = spawnArea.position;
        var spawnAreaSize = spawnArea.localScale;
        
        for (int i = 0; i < Mathf.Infinity; i++)
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
