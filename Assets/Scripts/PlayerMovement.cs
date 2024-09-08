using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class player : MonoBehaviour
{
    public float multiplier = 1;
    private float targetspeed = 10f;
    private float speedConstant ;
    public float acceleration = 1f; 
    public float deceleration = 1f;
    private float currentSpeedx = 0f;
    private float currentSpeedy = 0f;
    private float speedx, speedy;
    Rigidbody2D rb;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        speedConstant = targetspeed;


    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            targetspeed = 2 * speedConstant * multiplier;
          acceleration = 3;
        }
           
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            targetspeed = speedConstant * multiplier;
            acceleration = 1;
        }
          

        speedx = Input.GetAxisRaw("Horizontal");
        speedy = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(speedx) > 0 || Mathf.Abs(speedy) > 0)
        {   
            if (!Input.GetKey(KeyCode.LeftShift))
                 targetspeed = 10;

            if (Mathf.Abs(speedx) > 0 && Mathf.Abs(speedy) > 0)
            {
                targetspeed = targetspeed / Mathf.Sqrt(2);
            }

        }
        
        else
        {
            targetspeed = 0;
        }

       
        if (Mathf.Abs(currentSpeedx) <= targetspeed && speedx > 0)
        {
            currentSpeedx = Mathf.Lerp(currentSpeedx, targetspeed, acceleration * Time.deltaTime);
        }
        else if (Mathf.Abs(currentSpeedx) <= targetspeed && speedx < 0)
        {
            currentSpeedx = Mathf.Lerp(currentSpeedx, targetspeed*-1, acceleration * Time.deltaTime);
        }
        //X axis acceleration
        if (Mathf.Abs(currentSpeedx) >= targetspeed && speedx >= 0)
        {
            currentSpeedx = Mathf.Lerp(currentSpeedx, targetspeed, deceleration * Time.deltaTime);
        }
        else if (Mathf.Abs(currentSpeedx) >= targetspeed && speedx < 0)
        {
            currentSpeedx = Mathf.Lerp(currentSpeedx, targetspeed * -1, deceleration * Time.deltaTime);
        }
        //X axis deceleration

        if (Mathf.Abs(currentSpeedy) <= targetspeed && speedy > 0)
        {
            currentSpeedy = Mathf.Lerp(currentSpeedy, targetspeed, acceleration * Time.deltaTime);
        }
        else if (Mathf.Abs(currentSpeedy) <= targetspeed && speedy < 0)
        {
            currentSpeedy = Mathf.Lerp(currentSpeedy, targetspeed * -1, acceleration * Time.deltaTime);
        }
        //Y axis acceleration
        if (Mathf.Abs(currentSpeedy) >= targetspeed && speedy >= 0)
        {
            currentSpeedy = Mathf.Lerp(currentSpeedy, targetspeed, deceleration * Time.deltaTime);
        }
        else if (Mathf.Abs(currentSpeedy) >= targetspeed && speedy < 0)
        {
            currentSpeedy = Mathf.Lerp(currentSpeedy, targetspeed * -1, deceleration * Time.deltaTime);
        }
        //Y axis deceleration

        rb.velocity = new Vector2(currentSpeedx ,currentSpeedy);
    }
   
}