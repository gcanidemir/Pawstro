using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;
    public TextMeshProUGUI countText;
    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    // New field to control dragging behavior
    public bool isDraggable = true;

    public void InitialiseItem(Item newItem, bool draggable , int id)
    {
        item = newItem;
        isDraggable = draggable;  // Set the draggable flag
        image.sprite = newItem.image;
        CountRefresh();
        if (id > 5){
            gameObject.SetActive(false);
        }
    }

    public void CountRefresh()
    {

        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }
    public void StartFakeDrag()
    {
        // Simulate what OnBeginDrag does without requiring PointerEventData
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isDraggable) return;  // Prevent dragging if it's not draggable

        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable) return;  // Prevent dragging if it's not draggable

        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggable) return;  // Prevent dragging if it's not draggable

        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
}
