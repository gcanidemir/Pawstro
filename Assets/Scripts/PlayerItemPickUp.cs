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
        demoScript = GetComponent<DemoScript>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        pickedItem = collision.gameObject;
        if (collision.CompareTag("Item")){
            demoScript.PickItem(0);
            
            Destroy(pickedItem);
        }
    }


}
