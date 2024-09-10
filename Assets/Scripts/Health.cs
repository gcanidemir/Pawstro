using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxhealth = 100;
    public float currenthealth;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
        healthBar.SetMaxHealth(maxhealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            takedamage(25f);
        }

    }


    public void takedamage(float damage)
    {
        if (damage < currenthealth)
            currenthealth -= damage;
        else
            currenthealth = 0;
        healthBar.SetHealth(currenthealth);
    }


    public void Heal(float heal)
    {
        if (heal <= maxhealth - currenthealth)
            currenthealth += heal;
        else
            currenthealth = maxhealth;
        healthBar.SetHealth(currenthealth);
    }
}