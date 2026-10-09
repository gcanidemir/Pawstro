using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour, IInteractable

{
    [Header("UI")]
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject Background;
    [SerializeField] private Transform panel;

    [Header("Animation")]
    [SerializeField] private float openSpeed = 3f;
    [SerializeField] private float panelSize = 5f;
    [SerializeField] private float closeSpeedMultiplier = 9f;

    private const float ClosedTreshold = 0.005f;


    private bool isOpen;

    private Vector3 scaleChange;
    private float currentHeight = 0f;
    public bool ShowPrompt => !isOpen;
    public void Interact()
    {
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
    public void OnPlayerExit()
    {
        if (isOpen)
        {
            Close();
        }
    }

    void Start()
    {
        SetUIActive(false);
    }
    public void Open()
    {
        isOpen = true;
        SetUIActive(true);
        applyScaleChange();

    }
    public void Close()
    {
        isOpen = false;
    }


    private void Update()
    {
        if (!shopUI.activeSelf)
        {
            return;
        }
        float target = isOpen ? panelSize : 0f;
        float speed = isOpen ? openSpeed : openSpeed * closeSpeedMultiplier;
        currentHeight = Mathf.Lerp(currentHeight, target, speed * Time.deltaTime);
        applyScaleChange();

        if (!isOpen && currentHeight < ClosedTreshold)
        {
            SetUIActive(false);
            currentHeight = 0f;
        }

    }
    private void SetUIActive(bool active)
    {
        shopUI.SetActive(active);
        Background.SetActive(active);
    }
    private void applyScaleChange()
    {
        panel.localScale = new Vector3(panelSize, currentHeight, panelSize);
    }

}


