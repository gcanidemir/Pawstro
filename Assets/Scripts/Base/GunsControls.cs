using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsControls : MonoBehaviour
{
    public GameObject player;
    public GameObject Laser;
    public GameObject GunCam;
    private bool inArea =false;
    private bool usingGun = false;
    private Rigidbody2D rb;
    private readonly List<GameObject> savedActiveChildren = new List<GameObject>();
    void Start()
    {

    rb = player.GetComponent<Rigidbody2D>();
    Laser.SetActive(false);
    GunCam.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            inArea = false;
        }
    }

    public void ToggleGuns()
    {
        usingGun = !usingGun;
        Laser.SetActive(usingGun);
        GunCam.SetActive(usingGun);

        if (usingGun)
        {
            // Entering gun: remember what was active, then hide it
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            savedActiveChildren.Clear();

            foreach (Transform child in player.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    savedActiveChildren.Add(child.gameObject);
                    child.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            // Exiting gun: restore only what was active before
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            foreach (GameObject child in savedActiveChildren)
            {
                if (child != null) child.SetActive(true);
            }
            savedActiveChildren.Clear();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inArea)
        {
            ToggleGuns();
        }
    }
}
