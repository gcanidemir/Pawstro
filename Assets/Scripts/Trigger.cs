using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour

{
    [Header("Upgrade Stats")]
    public float oxlast = 1f;
    public float oxregen = 1f;
    public float HPregen = 1f;

    [Header("Rates")]
    [SerializeField] private float oxygenDrainPerSecond = 0.6f;
    [SerializeField] private float oxygenRegenPerSecond = 0.6f;
    [SerializeField] private float healthRegenPerSecond = 1.2f;
    [SerializeField] private float fuelRegenPerSecond = 6f;

    [Header("References")]
    [SerializeField] private player Player;
    [SerializeField] private Fuel fuel;
    [SerializeField] private Health health;
    [SerializeField] private Oxygen oxygen;

    private bool inSpace = true;

    private const string SafeZoneTag = "Heal";

    private void OnTriggerEnter2D(Collider2D other)
    {
         if (other.CompareTag(SafeZoneTag))
        inSpace = false;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
         if (other.CompareTag(SafeZoneTag))
        inSpace = true;

    }

    void Update()
    {
        float dt = Time.deltaTime;
 
        if (inSpace)
        {
            oxygen.takedamage(oxygenDrainPerSecond * dt / oxlast);
        }
        else
        {
            oxygen.Heal(oxygenRegenPerSecond * dt * oxregen);
            health.Heal(healthRegenPerSecond * dt * HPregen);
            fuel.Heal(fuelRegenPerSecond * dt * Player.fuelmod);
        }

    }
}
