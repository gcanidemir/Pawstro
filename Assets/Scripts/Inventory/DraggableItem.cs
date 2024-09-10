
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class DraggableItem: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;
    public TextMeshProUGUI countText;
    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;
    void Start(){
        InitialiseItem(item);
    }
    public void InitialiseItem(Item newItem){
        item = newItem;
        image.sprite = newItem.image;
        CountRefresh();
    }
    public void CountRefresh(){
        countText.text = count.ToString();
        bool textActive =count > 1;
        countText.gameObject.SetActive(textActive);
    }
    public void OnBeginDrag (PointerEventData eventData) {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }
    public void OnDrag (PointerEventData eventData) { 
        transform.position= Input.mousePosition;
    }
        public void OnEndDrag (PointerEventData eventData) { 
        transform.SetParent (parentAfterDrag);
        image.raycastTarget = true;
    }
}