using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIshop : MonoBehaviour
{
    public GameObject PlayerShop;
    public GameObject BaseShop;
    void Start()
    {
        PlayerShop.SetActive(true);
        BaseShop.SetActive(false);
    }

    public void EnablePlayerShop()
    {
        PlayerShop.SetActive(true);
        BaseShop.SetActive(false);
    }
    public void EnableBaseShop()
    {
        PlayerShop.SetActive(false);
        BaseShop.SetActive(true);
    }




}
