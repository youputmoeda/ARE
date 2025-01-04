using UnityEngine;
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
        // Verifica se a tecla de pausa foi pressionada
        if (_playerUIInput != null && _playerUIInput.escapePressed)
        {
            // Alterna entre pausar e retomar o jogo
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

            // Reseta o estado de escapePressed para evitar múltiplas chamadas
            _playerUIInput.escapePressed = false;
        }
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        SetCursorStateScript.SetCursorState(true);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        SetCursorStateScript.SetCursorState(false);
    }

    public void MainMenu()
    {
        // Garante que o tempo volte ao normal antes de sair para o menu principal
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
