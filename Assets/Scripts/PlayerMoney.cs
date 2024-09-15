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
    public GameObject MoneyError, LimitError;
    public TextMeshProUGUI Moneytext;
    private void Start()
    {
        Money = StartMoney;
        MoneyError.SetActive(false);
        LimitError.SetActive(false);
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
    public int SpendMoney(int amount, int limit, int currentlvl)
    {
        if (Money >= amount && currentlvl < limit)
        {
            Money -= amount;
            Moneytext.text = Money.ToString();
            return 1;
        }
        else if (Money >= amount && currentlvl == limit)
        {
            LimitMessage(2);
            return 0;
        }
        else
        {
            ErrorMessage(2);
            return 0;
        }
        void LimitMessage(float delayTime)
        {
            StartCoroutine(LimitAction(delayTime));
        }
        IEnumerator LimitAction(float delayTime)
        {
            LimitError.SetActive(true);
            yield return new WaitForSeconds(delayTime);
            LimitError.SetActive(false);
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
