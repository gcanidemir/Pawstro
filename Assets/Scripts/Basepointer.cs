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

    [Header("Fade")]
    [Tooltip("Seconds to fade fully in or out")]
    [SerializeField] private float fadeDuration = 0.5f;
    [Tooltip("Opacity when fully visible (0-1)")]
    [Range(0f, 1f)]
    [SerializeField] private float maxAlpha = 1f;

    private RectTransform arrowRect;
    private float alpha = 0f;
    private void Start()
    {
        if (cam == null) cam = Camera.main;
        arrowRect = arrowImage.rectTransform;

        alpha = 0f;
        ApplyAlpha();
    }

    private void LateUpdate()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(baseTarget.position);

        bool isFar = Vector2.Distance(player.position, baseTarget.position) > showDistance;
        bool isOnScreen =
            screenPos.x >= edgeMargin && screenPos.x <= Screen.width - edgeMargin &&
            screenPos.y >= edgeMargin && screenPos.y <= Screen.height - edgeMargin;


        bool shouldShow = isFar && !isOnScreen;

        float targetAlpha = shouldShow ? maxAlpha : 0f;
        alpha = Mathf.MoveTowards(alpha, targetAlpha, Time.deltaTime / fadeDuration);
        ApplyAlpha();

        if (alpha <= 0f) return;


        Vector2 center = new Vector2(Screen.width, Screen.height) * 0.5f;
        Vector2 direction = (Vector2)screenPos - center;
        if (direction.sqrMagnitude < 0.01f) return;


        Vector2 halfSize = center - new Vector2(edgeMargin, edgeMargin);
        float scale = Mathf.Min(halfSize.x / Mathf.Abs(direction.x), halfSize.y / Mathf.Abs(direction.y));


        arrowRect.position = center + direction * scale;


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowRect.rotation = Quaternion.Euler(0f, 0f, angle + spriteAngleOffset);
    }

    private void ApplyAlpha()
    {
        Color c = arrowImage.color;
        c.a = alpha;
        arrowImage.color = c;

        bool shouldRender = alpha > 0f;
        if (arrowImage.enabled != shouldRender)
            arrowImage.enabled = shouldRender;
    }
}
