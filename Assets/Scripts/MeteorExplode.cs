using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorExplode : MonoBehaviour
{
    public GameObject drop;
    public GameObject rareDrop;
    public int dropCount = 10;
    public float spread = 2f;
    public int rarity = 10;
    public int maxHealth = 4;
    int currentHealth;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame

    public void explode()
    {
        while (dropCount > 0)
        {
        int randomIndex = Random.Range(0,100);
            if (randomIndex > rarity) { 
            dropCount--;
            Vector3 pos = transform.position;
            pos.x += spread * UnityEngine.Random.value - spread / 2;
            pos.y += spread * UnityEngine.Random.value - spread / 2;
            GameObject go = Instantiate(drop);
            go.transform.position = pos;
            }
            else
            {
                dropCount--;
                Vector3 pos = transform.position;
                pos.x += spread * UnityEngine.Random.value - spread / 2;
                pos.y += spread * UnityEngine.Random.value - spread / 2;
                GameObject go = Instantiate(rareDrop);
                go.transform.position = pos;
            }
        }

        Destroy(gameObject);
    }

    public void takeDamage(int damage) {
        currentHealth -= damage;
        Debug.Log("Meteor taken damage");
            animator.SetInteger("Health", currentHealth);
        if(currentHealth <= 0)
        {
            explode();
        }
    }
}
