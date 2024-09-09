using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class BorderDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeeed;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
       textComponent.text = string.Empty;
        
        StartDialogue();
    }
    void Update(){
        if (Input.GetMouseButtonDown(0)){
            if (textComponent.text == lines[index]){
                if(index >= 4 && index <= 8){
                    index++;
                }
                NextLine();
            }
            else{
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }
    void StartDialogue(){
        int probability=Random.Range(0, 100);//decides on witch line will be used
        switch (probability)
    {
        case int n when (n >= 0 && n < 1):
            index = 4; 
            break;
        case int n when (n >= 1 && n <= 34):
            index = 1;
            break;
        case int n when (n > 34 && n <= 67):
            index = 2;
            break;
        default:
           index = 3;
            break;
    }

        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine(){
        foreach (char c in lines[index].ToCharArray()){
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeeed);
        }
    }
    void NextLine(){

        if(index != 0 && index < 4){
            index = 0;
            textComponent.text =string.Empty;
            StartCoroutine(TypeLine());
        }

        else if(index >= 4 && index <=8){
            textComponent.text =string.Empty;
            StartCoroutine(TypeLine()); 
        }

        else{
            gameObject.SetActive(false);
        }
        
    }
}
