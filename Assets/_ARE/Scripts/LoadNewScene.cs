using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    /// <summary>
    /// Método para carregar uma nova cena pelo nome.
    /// </summary>
    /// <param name="sceneName">Nome da cena a ser carregada</param>
    public void LoadSceneByName(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
            SetCursorStateScript.SetCursorState(false);
        }
        else
        {
            Debug.LogError("O nome da cena é inválido ou está vazio!");
        }
    }

    /// <summary>
    /// Método para carregar uma nova cena pelo índice.
    /// </summary>
    /// <param name="sceneIndex">Índice da cena a ser carregada</param>
    public void LoadSceneByIndex(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
            SetCursorStateScript.SetCursorState(false);
        }
        else
        {
            Debug.LogError("O índice da cena é inválido!");
        }
    }
}
