using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour, IInteractable

{
    [Header("UI")]
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject Background;
    //[SerializeField] private Transform panel; dont know what it does used to be tran

    [Header("Animation")]
    [SerializeField] private float openSpeed = 3f;
    [SerializeField] private float closeSpeedMultiplier = 9f;


    private const float ClosedTreshold = 0.005f;


    private bool isOpen;

    private Transform shopTransform;
    private Vector3 fullScale;
    private float heightPercent;
    public bool ShowPrompt => !isOpen;

    private void Awake()
    {
        shopTransform = shopUI.transform;
        fullScale = shopTransform.localScale;
    }


    private void Start()
    {
        heightPercent = 0f;
        applyScaleChange();
        SetUIActive(false);
    }

    // ── IInteractable ──
    public void Interact()
    {
        if (isOpen) { Close(); }
        else { Open(); }
    }

    public void OnPlayerExit()
    {
        Close();
    }


    public void Open()
    {
        isOpen = true;
        SetUIActive(true);
        applyScaleChange();

    }

    // Public so a "close" button in the shop UI can call it
    public void Close()
    {
        isOpen = false;
    }


    private void Update()
    {
        if (!shopUI.activeSelf) { return; }

        float target = isOpen ? 1f : 0f;
        float speed = isOpen ? openSpeed : openSpeed * closeSpeedMultiplier;

        heightPercent = Mathf.Lerp(heightPercent, target, speed * Time.deltaTime);
        applyScaleChange();

        if (!isOpen && heightPercent < ClosedTreshold)
        {
            SetUIActive(false);
            heightPercent = 0f;
        }

    }
    private void SetUIActive(bool active)
    {
        shopUI.SetActive(active);
        Background.SetActive(active);
    }
    private void applyScaleChange()
    {
        shopTransform.localScale = new Vector3(fullScale.x, fullScale.y*heightPercent, fullScale.z);
    }

}


