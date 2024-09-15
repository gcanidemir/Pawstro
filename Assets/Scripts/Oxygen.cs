using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour
{
    public float maxhealth = 100;
    public float currenthealth;
    public HealthBar healthBar;
    public Health player;
    public bool flag;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
        healthBar.SetMaxHealth(maxhealth);
    }

    private void Update()
    {
        if (currenthealth <= 0 && flag)
        {
            StartCoroutine(Ah());
        }
    }

    IEnumerator Ah()
    {
        flag = false;
        player.takedamage(2.0f);
        yield return new WaitForSeconds(2.0f);
        flag = true;
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