using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public float maxhealth;
    public float currenthealth;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
        healthBar.SetMaxHealth(maxhealth);
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