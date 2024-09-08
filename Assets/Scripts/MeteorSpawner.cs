using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject[] Meteors;

    void Start()
    {
        int meteorCount = Random.Range(25, 50);

        for (int i = 0; i < meteorCount; i++)
        {
            int randomIndex = Random.Range(0, 10);
            int randomMeteor;
            float radius;
            float angle = Random.Range(0f, 360f); 

            if (randomIndex >= 8 && randomIndex < 11)
            {
                randomMeteor = 2; 
                radius = Random.Range(19f, 24f); 
            }
            else if (randomIndex >= 4 && randomIndex < 8)
            {
                randomMeteor = 1; 
                radius = Random.Range(12f, 17f); 
            }
            else
            {
                randomMeteor = 0; 
                radius = Random.Range(5f, 10f); 
            }

            float posX = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float posY = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

            Vector3 randomSpawnPoint = new Vector3(posX, posY, 0); 
            Instantiate(Meteors[randomMeteor], randomSpawnPoint, Quaternion.identity); 
        }
    }
}
