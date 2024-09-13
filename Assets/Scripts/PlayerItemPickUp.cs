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
        if (collision.CompareTag("CommonOre")){
            demoScript.PickItem(0);
            Destroy(pickedItem);
        }
        if (collision.CompareTag("CommonGem")){
            demoScript.PickItem(1);
            Destroy(pickedItem);
        }
        if (collision.CompareTag("RareOre")){
            demoScript.PickItem(2);
            Destroy(pickedItem);
        }
        if (collision.CompareTag("RareGem")){
            demoScript.PickItem(3);
            Destroy(pickedItem);
        }
        if (collision.CompareTag("LegendaryOre")){
            demoScript.PickItem(4);
            Destroy(pickedItem);
        }
        if (collision.CompareTag("LegendaryGem")){
            demoScript.PickItem(5);
            Destroy(pickedItem);
        }
    }


}
