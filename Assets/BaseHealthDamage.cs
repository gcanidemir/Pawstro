using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealthDamage : MonoBehaviour
{
    public GameObject Turret1, Turret2, Turret3, Turret4, LaserDefense;
    public int turretlvl = 0;
    // Start is called before the first frame update
    public void Start()
    {
        Turret1.SetActive(false); Turret2.SetActive(false); Turret3.SetActive(false); Turret4.SetActive(false);
    }
    public void Update()
    {
        switch (turretlvl)
        {
            case 1:
                Turret1.SetActive(true);
                break;
            case 2:
                Turret2.SetActive(true);
                break;
            case 3:
                Turret3.SetActive(true);
                break;
            case 4:
                Turret4.SetActive(true);
                break;
        }
    }
}