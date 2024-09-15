using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    private float minDistanceBetweenEnemies = 3f;
    private float minRadius = 80f;
    private float maxRadius = 100f;

    private List<Vector3> spawnPoints = new List<Vector3>();

    public void SpawnEnemies(int enemyCount, bool doesSpawnMiniBoss, bool doesSpawnBoss)
    {

        for (int i = 0; i < enemyCount; i++)
        {
            float radius = Random.Range(minRadius,maxRadius);
            float angle = Random.Range(0f, 360f);
            int enemy;

            if(doesSpawnBoss)
            {
                enemy = 2;
                doesSpawnBoss = false;
            }
            else if(doesSpawnMiniBoss)
            {
                enemy = 1;
                doesSpawnMiniBoss = false;
            }
            else
            {
                enemy = 0;
            }

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
                Instantiate(enemyPrefabs[enemy], randomSpawnPoint, Quaternion.identity);
            }

        }
    }
}
