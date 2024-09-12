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
    AddPointerEvents(newItemGo, newItemDetailGo);

    // Update the dictionary with the new item
    if (itemGameObjectMap.ContainsKey(item))
    {
        // If item already exists, update the reference
        itemGameObjectMap[item] = newItemGo;
    }
    else
    {
        // Otherwise, add a new entry
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
    }

        public void RemoveItem(Item item)
    {
        Debug.Log($"Attempting to Remove Item: {item.name}");

        bool itemRemoved = false;

        // Iterate through all inventory slots to find and remove the item
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            ItemSlot slot = inventorySlots[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();

            if (itemInSlot != null && itemInSlot.item == item)
            {
                Debug.Log($"Found Item in Slot: {item.name}, Quantity: {itemInSlot.count}");

                // Remove the entire stack
                Destroy(itemInSlot.gameObject);
                itemGameObjectMap.Remove(item);
                Debug.Log($"Removed Entire Stack of {item.name}");

                itemRemoved = true;
            }
        }

        if (!itemRemoved)
        {
        Debug.LogError($"Item not found in inventory for removal: {item.name}");
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
