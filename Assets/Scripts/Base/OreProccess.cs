using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreProcess : MonoBehaviour
{
    public GameObject player;
    private bool inArea = false;
    public InventoryManager inventoryManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inArea = false;
        }
    }

    public void SellInventory()
    {
           List<Item> allItems = inventoryManager.GetAllItems();
            foreach (Item item in allItems)
            {
                inventoryManager.RemoveItem(item);
            }
        
            Debug.Log("Item removed from inventory");
        

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inArea)
        {
            SellInventory();
        }
    }
}
