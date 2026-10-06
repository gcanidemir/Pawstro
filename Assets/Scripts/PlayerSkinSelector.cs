using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerSkinSelector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject SelectionPart;
    [SerializeField] private Animator animator;

    [Header("Skins")]
    [SerializeField] private Button[] Buttons;
    [SerializeField] private AnimatorOverrideController[] skins;

    private void Start()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            int index = i; // Capture the current index for the button's onClick event
            Buttons[i].onClick.AddListener(() => SelectSkin(index));
        }
        
    }

    private void SelectSkin(int index)
    {
        if (index < 0 || index >= skins.Length)
        {
            Debug.LogWarning("Invalid skin index: " + index);
            return;
        }

        animator.runtimeAnimatorController = skins[index];
        SelectionPart.SetActive(false);
    }

}
