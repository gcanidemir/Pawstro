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
                NextLine();
            }
            else{
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }
    void StartDialogue(){
        index=Random.Range(1, 5);
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine(){
        foreach (char c in lines[index].ToCharArray()){
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeeed);
        }
    }
    void NextLine(){
        if(index != 0){
            index = 0;
            textComponent.text =string.Empty;
            StartCoroutine(TypeLine());
        }
        else{
            gameObject.SetActive(false);
        }
    }
}
