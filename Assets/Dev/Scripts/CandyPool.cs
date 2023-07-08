namespace Dev.Scripts
{
    using UnityEngine;
    using System.Collections.Generic;

    public class CandyPool : MonoBehaviour
    {
        public GameObject candyPrefab;
        public int initialPoolSize = 10;

        public List<GameObject> candyPool;

        private void Start()
        {
            candyPool = new List<GameObject>();
            CreateInitialPool();
            SignUpEvents();
        }

        private void SignUpEvents()
        {
            GameEvents.onSugarConsumed += ReturnCandyToPool;
        }

        private void CreateInitialPool()
        {
            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject candy = Instantiate(candyPrefab, transform.position, Quaternion.identity);
                candy.SetActive(false);
                candyPool.Add(candy);
            }
        }

        public GameObject GetCandyFromPool()
        {
            for (int i = 0; i < candyPool.Count; i++)
            {
                if (!candyPool[i].activeInHierarchy)
                {
                    candyPool[i].SetActive(true);
                    return candyPool[i];
                }
            }
            Debug.Log("Havuzda kullanılabilir bir şeker nesnesi bulunamadı!");

            return null;
        }

        public void ReturnCandyToPool(GameObject candy)
        {
            candy.SetActive(false);
            ObjectSpawner spawner = new ObjectSpawner();
            spawner._spawnedPositions.Remove(candy.gameObject.transform.position);
        }
    }

}