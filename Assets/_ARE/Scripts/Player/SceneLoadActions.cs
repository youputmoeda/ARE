using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoadActions : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private string _currentScene;
    
    int count = 0;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player")?.transform;

        if (_player == null)
        {
            Debug.LogError("Player não encontrado! Certifica-te de que o jogador tem a tag 'Player'.");
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_player == null || count >= SceneManager.loadedSceneCount)
        {
            Debug.LogWarning("Player ou condições inválidas. Não é possível mudar a posição.");
            return;
        }

        else if (SceneManager.GetActiveScene().name == _currentScene)
        {
            Debug.Log("Cena correta carregada, a mover o jogador.");
            StartCoroutine(SetPlayerPositionDelayed(_targetPosition));
        }

        else
        {
            Debug.Log($"Cena '{scene.name}' não corresponde a '{_currentScene}', não muda a posição do jogador.");
        }
        count++;
    }

    private IEnumerator SetPlayerPositionDelayed(Vector3 targetPosition)
    {
        yield return null; // Espera 1 frame
        _player.position = targetPosition;
        Debug.Log($"Nova posição do jogador após o atraso: {_player.position}");
    }
}
