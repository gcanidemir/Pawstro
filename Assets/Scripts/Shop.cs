using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour

{
    public GameObject shop;
    public GameObject Background;
    public GameObject ToolTip;
    public bool inShop = false;
    public bool shopOpenable = true;
    private Vector3 scaleChange;
    public Transform tran;
    private float x = 0f;
    public float OCT = 3f;
    public float ssize = 5f;
    void Start()
    {
        shop.SetActive(false);
        Background.SetActive(false);
    }
    public void EnableInv()
    {
        shop.SetActive(true);
        Background.SetActive(true);
    }
    public void disableInv()
    {
        shop.SetActive(false);
        Background.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        inShop = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inShop = false;
        shopOpenable = true;
    }

    private void Update()
    {
        if(inShop && shopOpenable)
        {
            ToolTip.SetActive(true);
        }
        else
        {
            ToolTip.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && inShop && shopOpenable)
        {
            EnableInv();
            shopOpenable = false;
            
      
        }

        else if (Input.GetKeyDown(KeyCode.E) && inShop && !shopOpenable)
        {
            shopOpenable = true;
        }
        if (!shopOpenable)
        {
            x = Mathf.Lerp(x, ssize, OCT * Time.deltaTime);
            scaleChange = new Vector3(ssize, x, ssize);
            tran.localScale = scaleChange;
        }
        else if (shopOpenable || inShop == false)
        {
            x = Mathf.Lerp(x, 0, 9 * OCT * Time.deltaTime);
            scaleChange = new Vector3(ssize, x, ssize);
            tran.localScale = scaleChange;
            if (x < 0.005)
            {
                disableInv();
            }
        }
        
    }

}


