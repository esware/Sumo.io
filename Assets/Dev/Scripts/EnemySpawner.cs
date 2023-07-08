using UnityEngine;
using UnityEngine.ProBuilder;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Oyuncunun yaratılacak düşman prefabı
    public Transform spawnArea; // Düşmanların yaratılacağı alanın transform bileşeni
    public int numberOfEnemies = 5; // Yaratılacak düşman sayısı

    private void Start()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        var meshFilter = spawnArea.GetComponent<MeshFilter>();
        var spawnPosition = Vector3.zero;
        if (meshFilter != null)
        {
            var size = meshFilter.mesh.bounds.size;
            Vector3 randomPoint = new Vector3(
                Random.Range(spawnPosition.x - size.x / 2.5f, spawnPosition.x + size.x / 2.5f),
                spawnPosition.y+10,
                Random.Range(spawnPosition.z - size.z / 2.5f, spawnPosition.z + size.z / 2.5f)
            );
            return randomPoint;
        }

        return Vector3.zero;
    }
}