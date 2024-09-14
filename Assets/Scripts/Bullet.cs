using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float force = 10f;  // Force to apply to the bullet
    public float lifetime = 5f;  // How long the bullet lasts

    private Rigidbody2D rb;

    void Start()
    {
        // Get Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Ensure Rigidbody2D exists
        if (rb != null)
        {
            // Apply force in the direction the bullet is facing (upward in local space)
            rb.AddForce(transform.up * force, ForceMode2D.Impulse);
        }

        // Destroy the bullet after its lifetime has passed
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Handle collision with enemies or other objects
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);  // Destroy enemy
            Destroy(gameObject);  // Destroy bullet
        }
    }
}