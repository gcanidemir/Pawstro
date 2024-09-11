using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreProccess : MonoBehaviour
{
    public GameObject player;
    private bool inArea =false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            inArea = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            inArea = false;
        }
    }
    public void SellInventory(){
            Debug.Log("a");
        
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.E) && inArea){
            SellInventory();
        }

        
    }
}
