using UnityEngine;
using UnityEngine.UI;  // For the Image component

public class ChangeSpriteAndImage : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer
    public Image uiImage;                  // Reference to the UI Image
    private Sprite newSprite;               // The new sprite to set

    void Update()
    {
        uiImage.sprite = spriteRenderer.sprite;
    }
}
