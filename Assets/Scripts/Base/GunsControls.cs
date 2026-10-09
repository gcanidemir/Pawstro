using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsControls : MonoBehaviour, IInteractable
{
    [Header("Player")]
    [SerializeField] private GameObject playerObject;

    [Header("Gun Mode")]
    [SerializeField] private GameObject Laser;
    [SerializeField] private GameObject GunCam;

    [Header("Others")]
    private Rigidbody2D rb;
    private player playerMovement;
    private RigidbodyConstraints2D savedConstraints;
    private bool usingGun = false;
    private readonly List<GameObject> hiddenChildren  = new List<GameObject>();

    public bool ShowPrompt => !usingGun;

    void Start()
    {

    rb = playerObject.GetComponent<Rigidbody2D>();
    playerMovement = playerObject.GetComponent<player>();

    Laser.SetActive(false);
    GunCam.SetActive(false);
    }

    public void Interact()
    {
        if(usingGun){ExitGuns();}
        else{EnterGuns();}
    }

    public void OnPlayerExit()
    {
        if(usingGun){ExitGuns();}
    }
    private void EnterGuns()
    {
        usingGun = true;
        Laser.SetActive(true);
        GunCam.SetActive(true);

        savedConstraints = rb.constraints;
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        hiddenChildren.Clear();
        foreach (Transform child in playerObject.transform)
        {
            if (child.gameObject.activeSelf)
            {
                hiddenChildren.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }
        }
    }

    private void ExitGuns()
    {
        usingGun = false;
        Laser.SetActive(false);
        GunCam.SetActive(false);

        rb.constraints = savedConstraints;

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        foreach (GameObject child in hiddenChildren)
        {
            if (child != null)
            {
                child.SetActive(true);
            }
        }
        hiddenChildren.Clear();
    }
    
}
