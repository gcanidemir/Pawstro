using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerSkinSelector : MonoBehaviour
{
    public GameObject SelectionPart;
    public Animator animator;
    public string animation1;
    public string animation2;
    public string animation3;
    public string animation4;
    private SpriteRenderer spriteRenderer;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    // Start is called before the first frame update
    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();

        //button1.onClick.AddListener(() => ApplySprite(skin1));
        //button2.onClick.AddListener(() => ApplySprite(skin2));
        //button3.onClick.AddListener(() => ApplySprite(skin3));
        //button4.onClick.AddListener(() => ApplySprite(skin4));
        button1.onClick.AddListener(() => ApplySprite(animation1));
        button2.onClick.AddListener(() => ApplySprite(animation2));
        button3.onClick.AddListener(() => ApplySprite(animation3));
        button4.onClick.AddListener(() => ApplySprite(animation4));
    }

    // Update is called once per frame
     private void ApplySprite(string animationName)
    {
        animator.Play(animationName);
        SelectionPart.SetActive(false);      
    }
}
