using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerSkinSelector : MonoBehaviour
{
    public GameObject SelectionPart;
    public Sprite skin1;
    public Sprite skin2;
    public Sprite skin3;
    public Sprite skin4;
    private SpriteRenderer spriteRenderer;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        button1.onClick.AddListener(() => ApplySprite(skin1));
        button2.onClick.AddListener(() => ApplySprite(skin2));
        button3.onClick.AddListener(() => ApplySprite(skin3));
        button4.onClick.AddListener(() => ApplySprite(skin4));
    }

    // Update is called once per frame
     private void ApplySprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        SelectionPart.SetActive(false);      
    }
}
