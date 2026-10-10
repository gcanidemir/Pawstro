using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basepointer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform baseTarget;
    [SerializeField] private Image arrowImage;
    private Camera cam;

    [Header("Settings")]
    [Tooltip("Arrow appears when the player is farther than this from the base")]
    [SerializeField] private float showDistance = 15f;
    [Tooltip("Distance in pixels between the arrow and the screen edge")]
    [SerializeField] private float edgeMargin = 50f;
    [Tooltip("-90 if your arrow sprite points UP, 0 if it points RIGHT")]
    [SerializeField] private float spriteAngleOffset = -90f;

    private RectTransform arrowRect;
    private void Start()
    {
        if (cam == null) cam = Camera.main;
        arrowRect = arrowImage.rectTransform;
    }

    private void LateUpdate()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(baseTarget.position);
        bool isFar = Vector2.Distance(player.position, baseTarget.position) > showDistance;
        bool isOnScreen = 
        screenPos.x >= edgeMargin && screenPos.x <= Screen.width - edgeMargin &&
        screenPos.y >= edgeMargin && screenPos.y <= Screen.height - edgeMargin;

        bool visible = isFar && !isOnScreen;
        if (arrowImage.enabled != visible) arrowImage.enabled = visible;

        if (!visible) return;

        Vector2 center = new Vector2(Screen.width, Screen.height) *0.5f;
        Vector2 direction = (Vector2)screenPos - center;

        Vector2 halfSize = center - new Vector2(edgeMargin, edgeMargin);
        float scale = Mathf.Min(halfSize.x / Mathf.Abs(direction.x), halfSize.y / Mathf.Abs(direction.y));
        arrowRect.position = center + direction * scale;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowRect.rotation = Quaternion.Euler(0f, 0f, angle + spriteAngleOffset);
        
    }
}
