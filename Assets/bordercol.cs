using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bordercol : MonoBehaviour
{
    public GameObject text; // UI element or other game object
    public float bounceBackDistance = 9f; // The amount to move the player back

    public int mulx ;
    public int muly ;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug log for detecting collisions
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if(gameObject.transform.position.x > 0){
            mulx= 1;
        }
        else if(gameObject.transform.position.x < 0){
            mulx = -1;
        }
        if(gameObject.transform.position.y > 0){
            muly= 1;
        }
        else if(gameObject.transform.position.y < 0){
            muly = -1;
        }

        // Check if collided with a Side Border
        if (collision.CompareTag("Border"))
        {
            text.SetActive(true); // Display the text or message

            // Move the player back by a fixed amount on the x-axis
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - bounceBackDistance*mulx, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        // Check if collided with a Border
        else if (collision.CompareTag("SideBorder"))
        {
            text.SetActive(true); // Display the text or message

            // Move the player back by a fixed amount on the y-axis
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y*muly - bounceBackDistance, gameObject.transform.position.z);
        }
    }
}
