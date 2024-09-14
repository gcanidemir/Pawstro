using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsLookAt : MonoBehaviour
{
    public float min;
    public float max;
    public GameObject bulletPrefab;  // Bullet prefab to instantiate
    public Transform firePoint;      // The position from where the bullets are fired
    private Rigidbody2D rb;
    private bool InBound = false;
    private bool isShooting = false;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Freeze X and Y movement
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        GameObject closestEnemy = FindClosestEnemy();

        if (closestEnemy != null)
        {
            // Calculate the direction to the closest enemy
            Vector3 direction = closestEnemy.transform.position - transform.position;

            // Calculate the angle in degrees (relative to the X-axis)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Check if the angle is within bounds
            if (angle <= max && angle >= min)
            {
                InBound = true;
            }
            else
            {
                InBound = false;
            }

            // Clamp the angle to be within the range of min and max
            float clampedAngle = Mathf.Clamp(angle, min, max);

            // Apply the rotation with clamped angle, subtracting 90 to align properly
            transform.rotation = Quaternion.Euler(0, 0, clampedAngle - 90);
        }

        // If in bounds and not already shooting, start shooting
        if (InBound && !isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    // Coroutine to handle shooting every 3 seconds
    IEnumerator Shoot()
    {
        isShooting = true;

        while (InBound)
        {
            // Instantiate the bullet at the firePoint position
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Wait for 3 seconds before shooting again
            yield return new WaitForSeconds(3f);
        }

        isShooting = false;
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