using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour

{
    public player Player;
    public Fuel fuel;
    public Health health;
    public Oxygen oxygen;
    public float oxregen = 1f;
    public float oxlast = 1f;
    public float HPregen = 1f;
    public bool inspace = true;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Heal")
        {
            inspace = false;
        }
    }
        private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Heal")
        {
            inspace = true;
        }

    }

    void Update()
    {
        if (inspace)
            oxygen.takedamage(0.01f / oxlast);
        else if (!inspace)
        {
            oxygen.Heal(0.01f * oxregen);
        health.Heal(0.02f * HPregen);
        fuel.Heal(0.1f * Player.fuelmod);
         }
       
    }
}
