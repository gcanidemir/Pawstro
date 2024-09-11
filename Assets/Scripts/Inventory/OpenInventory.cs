using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public GameObject inventory;
    void Start(){
        inventory.SetActive(false);
    }
    // Start is called before the first frame update
    public void EnableInventory(){
        inventory.SetActive(true);
    }
    public void disableInv()
    {
        inventory.SetActive(false);
    }
}
