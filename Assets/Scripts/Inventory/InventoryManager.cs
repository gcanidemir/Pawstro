using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    public int maxStack;
    public GameObject inventoryItemPrefab;
    public GameObject inventoryItemDetailsPrefab;
    public ItemSlot[] inventorySlots;
    public DemoScript demoScript;

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
        // Instantiate the main item
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        DraggableItem inventoryItem = newItemGo.GetComponentInChildren<DraggableItem>();
        inventoryItem.InitialiseItem(item, true, id);
        int detailId = id + 6;
        demoScript.Deneme(newItemGo, detailId);
        GameObject newItemDetailGo = SpawnNewItemDetail(demoScript.Deneme(newItemGo, detailId), newItemGo, detailId);
        AddPointerEvents(newItemGo, newItemDetailGo);
        
    }

    GameObject SpawnNewItemDetail(Item item, GameObject parent, int detailId)
{
    // Instantiate the item detail as a child of the parent item
    GameObject newItemDetailGo = Instantiate(inventoryItemDetailsPrefab, parent.transform);
    DraggableItem inventoryItemDetail = newItemDetailGo.GetComponentInChildren<DraggableItem>();

    if (inventoryItemDetail != null)
    {
        // Initialize the item detail
        inventoryItemDetail.InitialiseItem(item, false, detailId);
    }

    Vector3 worldPosition = new Vector3(900, 250, 0);
        Vector3 localPosition = parent.transform.InverseTransformPoint(worldPosition);
        newItemDetailGo.transform.localPosition = localPosition;

    return newItemDetailGo;
}



    void AddPointerEvents(GameObject item, GameObject child)
    {
        EventTrigger trigger = item.AddComponent<EventTrigger>();
        EventTrigger.Entry enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener((eventData) => { OnPointerEnter(child); });
        trigger.triggers.Add(enterEntry);

        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener((eventData) => { OnPointerExit(child); });
        trigger.triggers.Add(exitEntry);
    }

    void OnPointerEnter(GameObject child)
    {
        child.SetActive(true);
    }

    void OnPointerExit(GameObject child)
    {
        child.SetActive(false);
    }

    public void RemoveItem(Item item)
    {
        // Implement item removal logic here
    }
    
}
