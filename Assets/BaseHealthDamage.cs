using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealthDamage : MonoBehaviour
{
    public GameObject Turret1, Turret2, Turret3, Turret4, LaserDefense;
    // Start is called before the first frame update
    public void Start()
    {
        //Turret1.SetActive(false); Turret2.SetActive(false); Turret3.SetActive(false); Turret4.SetActive(false);
        LaserDefense.SetActive(false);
    }

}