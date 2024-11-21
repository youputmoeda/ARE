using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        int lastIndex = SceneManager.sceneCountInBuildSettings - 2;
        SceneManager.LoadScene(lastIndex);
    }

    public void QuitGame()
    {

        Debug.Log("QUIT");
        Application.Quit();
    }
        

} 
