using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryManager : MonoBehaviour
{
    public int maxStack;
    public GameObject inventoryItemPrefab;
    public GameObject inventoryItemDetailsPrefab;
    public ItemSlot[] inventorySlots;
    public DemoScript demoScript;
    public PlayerMoney moneyScript;
    public float proccessmodifier;
    private Dictionary<Item, GameObject> itemGameObjectMap = new Dictionary<Item, GameObject>();

    public bool AddItem(Item item, int id)
    {
        // First check if it's an item (ID 0-5)
        if(id >= 0 && id <= 5){
            // Try to add the item to the inventory
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                ItemSlot slot = inventorySlots[i];
                DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();

                if (itemInSlot != null && itemInSlot.item == item
                    && itemInSlot.count < maxStack && itemInSlot.item.stackable)
                {
                    itemInSlot.count++;
                    itemInSlot.CountRefresh();
                    return true;
                }
            }

            for (int i = 0; i < inventorySlots.Length; i++)
            {
                ItemSlot slot = inventorySlots[i];
                DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();

                if (itemInSlot == null)
                {
                    // Spawn the new item and its detail
                    SpawnNewItem(item, slot, id);
                    return true;
                }
            }
        }
        return false;
    }

    void SpawnNewItem(Item item, ItemSlot slot, int id)
    {
    GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
    DraggableItem inventoryItem = newItemGo.GetComponentInChildren<DraggableItem>();
    inventoryItem.InitialiseItem(item, true, id);
    int detailId = id + 6;
    GameObject newItemDetailGo = SpawnNewItemDetail(demoScript.Deneme(newItemGo, detailId), newItemGo, detailId);

    if (itemGameObjectMap.ContainsKey(item))
    {
        itemGameObjectMap[item] = newItemGo;
    }
    else
    {
        itemGameObjectMap.Add(item, newItemGo);
    }
}

    GameObject SpawnNewItemDetail(Item item, GameObject parent, int detailId)
    {
        GameObject newItemDetailGo = Instantiate(inventoryItemDetailsPrefab, parent.transform);
        DraggableItem inventoryItemDetail = newItemDetailGo.GetComponentInChildren<DraggableItem>();

        if (inventoryItemDetail != null)
        {
            inventoryItemDetail.InitialiseItem(item, false, detailId);
        }

        AddPointerEvents(parent, newItemDetailGo);
        String DetailName = "";
        String DetailPrice = "";
        switch (detailId){
            case 6:
            DetailName = "Copper";
            DetailPrice = "5";
            break;
            case 7: 
            DetailName = "Gypsum";
            DetailPrice = "15";
            break;
            case 8:
            DetailName = "Sulfur";
            DetailPrice = "10";
            break;
            case 9:
            DetailName = "Bitane";
            DetailPrice = "25";
            break;
            case 10:
            DetailName = "Galena";
            DetailPrice = "20";
            break;
            default:
            DetailName = "Endite";
            DetailPrice = "50";
            break;
        }

        TMP_Text[] tmpTexts = newItemDetailGo.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text tmpText in tmpTexts)
        {
           if (tmpText.name == "Name")  // Assuming this is the name Text object
        {
            tmpText.text = DetailName;  // Set the item name
        }
        else if (tmpText.name == "Money")  // Assuming this is the price Text object
        {
            tmpText.text = DetailPrice;  // Set the price (replace with actual price logic)
        }
        }
        
        return newItemDetailGo;
    }

    void AddPointerEvents(GameObject item, GameObject child)
    {
        EventTrigger trigger = item.AddComponent<EventTrigger>();

        EventTrigger.Entry enterEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        enterEntry.callback.AddListener((eventData) => { OnPointerEnter(child); });
        trigger.triggers.Add(enterEntry);

        EventTrigger.Entry exitEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        exitEntry.callback.AddListener((eventData) => { OnPointerExit(child); });
        trigger.triggers.Add(exitEntry);
    }

    void OnPointerEnter(GameObject child)
    {
        child.SetActive(true);
         DraggableItem draggableItem = child.GetComponentInChildren<DraggableItem>();
            draggableItem.StartFakeDrag();
        
    }

    void OnPointerExit(GameObject child)
    {
        child.SetActive(false);
        DraggableItem draggableItem = child.GetComponentInChildren<DraggableItem>();
            draggableItem.EndFakeDrag();
    }

        public void RemoveItem(Item item)
    {

        // Iterate through all inventory slots to find and remove the item
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            ItemSlot slot = inventorySlots[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();

            if (itemInSlot != null && itemInSlot.item == item)
            {
                Transform itemDetailTransform = itemInSlot.transform.Find("ItemDetail(Clone)");
                GameObject newItemDetailGo = itemDetailTransform.gameObject;
                TMP_Text[] tmpTexts = newItemDetailGo.GetComponentsInChildren<TMP_Text>();
                foreach (TMP_Text tmpText in tmpTexts)
                {
                    if (tmpText.name == "Money")
                    {
                        string text = tmpText.text;
                        int total = int.Parse(text);
                        float totalstackprice = total * itemInSlot.count * (proccessmodifier+1);
                        moneyScript.EarnMoney((int)totalstackprice);
                        Debug.Log(totalstackprice + " " + total);
                    }
                    
                }

                // Remove the entire stack
                Destroy(itemInSlot.gameObject);
                itemGameObjectMap.Remove(item);

            }
        }
    }
    public List<Item> GetAllItems()
    {
        List<Item> items = new List<Item>();
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            ItemSlot slot = inventorySlots[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();

            if (itemInSlot != null && itemInSlot.item != null && !items.Contains(itemInSlot.item))
            {
                items.Add(itemInSlot.item);
            }
        }
        return items;
    }

    
}
