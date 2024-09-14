using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit?");
    }

    public void LoadGame(string gameName)
    {
        SceneManager.LoadScene(gameName);                                                                                                                                                                                                                                                                                                                                             
    }
}
