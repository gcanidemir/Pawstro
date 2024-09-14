using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialCharSelect : MonoBehaviour
{
    public Animator animator;
    public int chars = 1,currenttext = 1;
    public GameObject TextBox,text1,text2,text3,text4,text5,text6,text7,text8;
    public bool tutorialtext = false;
    public float x = 0;
    public float y = 0;
    private float OCT = 9;
    private Vector3 scaleChange;
    public Transform tran;
    public Image blackscreen;
    private void Start()
    {
        TextBox.SetActive(false);
        Color color = blackscreen.color;
        color.a = 0;
        blackscreen.color = color;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && tutorialtext == true)
        {
            currenttext++;
        }
        if (Input.GetKeyDown(KeyCode.Q) && currenttext != 9 && tutorialtext == true)
        {
            currenttext--;
        }

        if (currenttext < 1)
        {
            currenttext = 1;
        }

        switch (currenttext)
        {
            case 1:
                text1.SetActive(true);
                text2.SetActive(false);
                break;
            case 2:
                text1.SetActive(false);
                text2.SetActive(true);
                text3.SetActive(false);
                break;
            case 3:
                text2.SetActive(false);
                text3.SetActive(true);
                text4.SetActive(false);
                break;
            case 4:
                text3.SetActive(false);
                text4.SetActive(true);
                text5.SetActive(false);
                break;
            case 5:
                text4.SetActive(false);
                text5.SetActive(true);
                text6.SetActive(false);
                break;
            case 6:
                text5.SetActive(false);
                text6.SetActive(true);
                text7.SetActive(false);
                break;
            case 7:
                text6.SetActive(false);
                text7.SetActive(true);
                text8.SetActive(false);
                break;
            case 8:
                text7.SetActive(false);
                text8.SetActive(true);
                break;
             case 9:
                tutorialtext = false;
                x = Mathf.Lerp(x, 0,OCT * Time.deltaTime);
            scaleChange = new Vector3(3.13f, x, 3.13f);
            tran.localScale = scaleChange;
                if ( x < 0.03)
                {
                    TextBox.SetActive(false);
                    Color color = blackscreen.color;
                    y = Mathf.Lerp(y, 1, OCT * Time.deltaTime);
                    color.a = y;
                    blackscreen.color = color;
                    toGame(1);
                }

                break;
    
        }
        ErrorMessage(2);

        if (tutorialtext == true) 
        {  
            x = Mathf.Lerp(x, 3.13f,OCT * Time.deltaTime);
            scaleChange = new Vector3(3.13f, x, 3.13f);
            tran.localScale = scaleChange;
        }
    }
    void ErrorMessage(float delayTime)
    {
        StartCoroutine(DelayAction(delayTime));
    }
    void toGame(float delayTime)
    {
        StartCoroutine(DelayOpen(delayTime));
    }
    IEnumerator DelayOpen(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
         SceneManager.LoadScene("MainGame");
    }
    IEnumerator DelayAction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        tutorialtext = true;
        TextBox.SetActive(true);
    }
}
