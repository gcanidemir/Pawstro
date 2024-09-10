using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemPickUp : MonoBehaviour
{
    private GameObject pickedItem;
    private DemoScript demoScript;
    
    // Start is called before the first frame update
    void Start()
    {
        // Check if the DemoScript is already attached to the GameObject
        demoScript = GetComponent<DemoScript>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        pickedItem = collision.gameObject;
        if (collision.CompareTag("Item")){
            demoScript.PickItem(4);
            Destroy(pickedItem);
        Debug.Log("aldim");
        }
        Debug.Log("Verdim");
        Debug.Log("Ben seni yendim");
    }


}
