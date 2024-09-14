using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform player;
    public HealthBar healthBar;
    public GameObject TpHud;
    public bool CanTeleport = false;
    public float TeleportCoolDown = 0f;


    public void Start()
    {
        TpHud.SetActive(false);
    }
    public void Update()
    {
        if (CanTeleport) 
        {
            TpHud.SetActive(true);
        }
        else
            TpHud.SetActive(false);

        if (TeleportCoolDown > 0)
        {
            TeleportCoolDown -= Time.deltaTime;
            healthBar.SetHealth(TeleportCoolDown);
        }
        if (TeleportCoolDown < 0f)
        {
            TeleportCoolDown = 0;
        }

        if (CanTeleport == true && TeleportCoolDown == 0)
        {
            if (Input.GetKey(KeyCode.B))
                {
                    player.localPosition = Vector3.zero;
                TeleportCoolDown = 15;
                healthBar.SetMaxHealth(TeleportCoolDown);
                healthBar.SetHealth(TeleportCoolDown);
                }
        }

    }
   




    
}
