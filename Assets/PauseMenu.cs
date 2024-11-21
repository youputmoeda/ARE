using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused = false;

    private PlayerUIInput _playerUIInput;

   private void Awake()
   {
        _playerUIInput = GetComponent<PlayerUIInput>();

   }

    void Start()
    {
            pauseMenu.SetActive(false);
        
    }

   

    private void Update()
    {
         if (_playerUIInput != null && _playerUIInput.escapePressed)
       // if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    private void PauseGame()
    {        
     pauseMenu.SetActive(true);
     Time.timeScale = 0f;
     isPaused = true;
    
     Cursor.lockState = CursorLockMode.None;
     Cursor.visible = true;
    }


    public void ResumeGame()
    {
     
     pauseMenu.SetActive(false);
     Time.timeScale = 1f;
     isPaused = false;

     Cursor.lockState = CursorLockMode.Locked;
     Cursor.visible = false;
    }

    public void MainMenu()
    {
     SceneManager.LoadScene(0);
    }



    public void QuitGame()
    {
     Application.Quit();
    }

        
}