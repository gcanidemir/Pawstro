using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMoney : MonoBehaviour
{
    public int Money;
    public int StartMoney;
    public float ErrorTimer = 0f;
    public float ErrorInterval = 120f;
    public GameObject MoneyError;
    public TextMeshProUGUI Moneytext;
    private void Start()
    {
        Money = StartMoney;
        MoneyError.SetActive(false);
        Moneytext.text = Money.ToString();
    }
    public void SetMoney(int amount)
    {
        Money = amount;
        Moneytext.text = Money.ToString();
    }
    public void EarnMoney(int amount)
    {
        Money += amount;
        Moneytext.text = Money.ToString();
    }
    public int SpendMoney(int amount)
    {
        if(Money >=amount)
        {
            Money -= amount;
            Moneytext.text = Money.ToString();
            return 1;
        }
        else
        {
            ErrorMessage(2);
            return 0;
        }
       
    }
    void ErrorMessage(float delayTime)
    {
        StartCoroutine(DelayAction(delayTime));
    }
    IEnumerator DelayAction(float delayTime)
    {
        MoneyError.SetActive(true);
        yield return new WaitForSeconds(delayTime);
        MoneyError.SetActive(false);
    }
}
