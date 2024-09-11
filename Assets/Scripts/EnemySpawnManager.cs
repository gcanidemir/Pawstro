using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private float minDistanceBetweenEnemies = 1f;
    private float minRadius = 10f;
    private float maxRadius = 30f;

    private List<Vector3> spawnPoints = new List<Vector3>();

    private void EnemySpawnManagerInit()
    {
        int enemyCount = 100;

        for (int i = 0; i < enemyCount; i++)
        {
            float radius = 50f;
            float angle = Random.Range(0f, 360f);

            float posX = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float posY = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            Vector3 randomSpawnPoint = new Vector3(posX, posY, 0);

            bool validSpawn = false;
            int attempts = 0;

            while (!validSpawn && attempts < 100)
            {
                validSpawn = true;
                foreach (Vector3 spawn in spawnPoints)
                {
                    if (Vector3.Distance(spawn, randomSpawnPoint) < minDistanceBetweenEnemies)
                    {
                        validSpawn = false;
                        break;
                    }
                }

                attempts++;
            }

            if (validSpawn)
            {
                spawnPoints.Add(randomSpawnPoint);
                Instantiate(enemyPrefab, randomSpawnPoint, Quaternion.identity);
            }

        }
    }

    void Start()
    {
        EnemySpawnManagerInit();   
    }

    void Update()
    {
        
    }
}
