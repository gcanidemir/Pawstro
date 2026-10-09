using System.Collections.Generic;
using UnityEngine;

// Goes on the PLAYER. Detects interactable zones, shows the tooltip, handles the E key.
public class PlayerInteractor : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [Header("UI")]
    [SerializeField] private GameObject ToolTip;

    [Header("Performance")]
    [SerializeField] private float checkInterval = 0.1f;// How often to check for interactables in range

    [Header("Debug")]
    [SerializeField] private bool debugLogs = true;

    private Rigidbody2D rb;
    private ContactFilter2D contactFilter;
    private float checkTimer;
    private readonly List<Collider2D> hits = new List<Collider2D>();
    private readonly HashSet<IInteractable> foundInteractables = new HashSet<IInteractable>();
    private readonly List<IInteractable> inRange = new List<IInteractable>();
    private IInteractable Current => inRange.Count > 0 ? inRange[inRange.Count - 1] : null;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        contactFilter.NoFilter();
    }

    private void Update()
    {
        bool interactPressed = Input.GetKeyDown(interactKey);

        checkTimer -= Time.deltaTime;
        if (checkTimer <= 0f)
        {
            checkTimer = checkInterval;
            RefreshInRange();
        }

        IInteractable current = Current;

        bool showTip = current != null && current.ShowPrompt;
        if (ToolTip.activeSelf != showTip)
            ToolTip.SetActive(showTip);

        if (current != null && Input.GetKeyDown(interactKey))
        {
            if (debugLogs) Debug.Log($"[Interactor] E pressed -> {((MonoBehaviour)current).name}");
            current.Interact();
        }
    }

    private void RefreshInRange()
    {
        foundInteractables.Clear();
        rb.OverlapCollider(contactFilter, hits);
        foreach (Collider2D hit in hits)
        {
            IInteractable interactable = hit.GetComponentInParent<IInteractable>();
            if (interactable != null)
                foundInteractables.Add(interactable);
        }

        for (int i = inRange.Count - 1; i >= 0; i--)
        {
            IInteractable interactable = inRange[i];
            if (foundInteractables.Contains(interactable)) continue;

            inRange.RemoveAt(i);
            interactable.OnPlayerExit();
            if (debugLogs) Debug.Log($"[Interactor] Exited {((MonoBehaviour)interactable).name}");
        }

        foreach (IInteractable interactable in foundInteractables)
        {
            if (inRange.Contains(interactable)) continue;

            inRange.Add(interactable);
            if (debugLogs) Debug.Log($"[Interactor] Entered {((MonoBehaviour)interactable).name}");
        }
    }
}

