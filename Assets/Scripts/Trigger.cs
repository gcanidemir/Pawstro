using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour

{
    public Health health;
    public Oxygen oxygen;
    public float damagetimer = 0f;
    public float damageInterval = 2f;
    public float oxregen = 1f;
    public float oxlast = 1f;
    public float HPregen = 1f;
    public bool inspace = true;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            damagetimer += Time.deltaTime;
            if (damagetimer >= damageInterval)
            {
                health.takedamage(20);
                Debug.Log("ah!");
                damagetimer = 0f;
            }
            
        }
        else
        
        if (collision.tag == "Heal")
        {
                health.Heal(0.02f*HPregen);
                Debug.Log("Yuppie!");         
        }


    }
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
        damagetimer = 0f;

    }


    void Update()
    {
        if (inspace)
            oxygen.takedamage(0.01f/oxlast);
        else if (!inspace)
            oxygen.Heal(0.01f*oxregen);



    }
}
