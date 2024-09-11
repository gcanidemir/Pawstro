using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsLookAt : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Freeze X and Y movement
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        // Find the closest enemy
        GameObject closestEnemy = FindClosestEnemy();

        if (closestEnemy != null)
        {
            // Calculate the direction to the closest enemy
            Vector3 direction = closestEnemy.transform.position - transform.position;

            // Calculate the angle to rotate towards the closest enemy
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Apply the rotation on the Z axis while keeping X and Y positions frozen
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
        }
    }

    // Method to find the closest enemy
    GameObject FindClosestEnemy()
    {
        // Find all enemies tagged "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        // Loop through all enemies to find the closest one
        foreach (GameObject enemy in enemies)
        {
            // Calculate the distance to the enemy
            float distance = Vector3.Distance(enemy.transform.position, currentPosition);

            // If this enemy is closer than the previous one, set it as the closest
            if (distance < minDistance)
            {
                closest = enemy;
                minDistance = distance;
            }
        }

        return closest;
    }
}

