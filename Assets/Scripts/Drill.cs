using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public bool canFire;
    private float timer;
    public float timeBetweenFire;
    // Start is called before the first frame update
    void Start()
    {
        camTake();
    }

    // Update is called once per frame
    void Update()
    {
        look();
        fire();
    }
    private void camTake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    private void look()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    private void fire()
    {
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            Debug.Log("týkladýn");
            canFire = false;
        }
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFire)
            {
                canFire = true;
                timer = 0;
            }

        }
    }
}
