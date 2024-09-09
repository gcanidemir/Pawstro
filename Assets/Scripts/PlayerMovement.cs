using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class player : MonoBehaviour
{
    public float multiplier = 1;
    private float targetspeedX = 10f;
    private float targetspeedY = 10f;

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
        speedConstant = 10;


    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            targetspeedX = 2 * speedConstant * multiplier;
            targetspeedY = 2 * speedConstant * multiplier;

          acceleration = 3;
        }
           
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            targetspeedX = speedConstant * multiplier;
            targetspeedY = speedConstant * multiplier;
            acceleration = 1;
        }
          

        speedx = Input.GetAxisRaw("Horizontal");
        speedy = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(speedx) > 0)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
                 targetspeedX = 10;

            if (Mathf.Abs(speedx) > 0 && Mathf.Abs(speedy) > 0)
            {
                targetspeedX = targetspeedX / Mathf.Sqrt(2);
            }     

        }

        else
        {
            targetspeedX = 0;
        }

        if(Mathf.Abs(speedy) > 0){
            if (!Input.GetKey(KeyCode.LeftShift))
                 targetspeedY = 10;

            if (Mathf.Abs(speedx) > 0 && Mathf.Abs(speedy) > 0)
            {
                targetspeedY = targetspeedY / Mathf.Sqrt(2);
            }
        }

        else
        {
            targetspeedY = 0;
        }

       
        if (Mathf.Abs(currentSpeedx) <= targetspeedX && speedx > 0)
        {
            currentSpeedx = Mathf.Lerp(currentSpeedx, targetspeedX, acceleration * Time.deltaTime);
        }
        else if (Mathf.Abs(currentSpeedx) <= targetspeedX && speedx < 0)
        {
            currentSpeedx = Mathf.Lerp(currentSpeedx, targetspeedX*-1, acceleration * Time.deltaTime);
        }
        //X axis acceleration
        if (Mathf.Abs(currentSpeedx) >= targetspeedX && speedx >= 0)
        {
            currentSpeedx = Mathf.Lerp(currentSpeedx, targetspeedX, deceleration * Time.deltaTime);
        }
        else if (Mathf.Abs(currentSpeedx) >= targetspeedX && speedx < 0)
        {
            currentSpeedx = Mathf.Lerp(currentSpeedx, targetspeedX * -1, deceleration * Time.deltaTime);
        }
        //X axis deceleration

        if (Mathf.Abs(currentSpeedy) <= targetspeedY && speedy > 0)
        {
            currentSpeedy = Mathf.Lerp(currentSpeedy, targetspeedY, acceleration * Time.deltaTime);
        }
        else if (Mathf.Abs(currentSpeedy) <= targetspeedY && speedy < 0)
        {
            currentSpeedy = Mathf.Lerp(currentSpeedy, targetspeedY * -1, acceleration * Time.deltaTime);
        }
        //Y axis acceleration
        if (Mathf.Abs(currentSpeedy) >= targetspeedY && speedy >= 0)
        {
            currentSpeedy = Mathf.Lerp(currentSpeedy, targetspeedY, deceleration * Time.deltaTime);
        }
        else if (Mathf.Abs(currentSpeedy) >= targetspeedY && speedy < 0)
        {
            currentSpeedy = Mathf.Lerp(currentSpeedy, targetspeedY * -1, deceleration * Time.deltaTime);
        }
        //Y axis deceleration

        rb.velocity = new Vector2(currentSpeedx ,currentSpeedy);
    }
   
}