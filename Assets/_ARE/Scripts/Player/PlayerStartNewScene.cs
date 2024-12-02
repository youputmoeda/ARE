using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerStartNewScene : MonoBehaviour
{
    [System.Serializable]
    public class ControllerSettings
    {
        public GameObject mesh;
        public Camera camera;
        public Vector3 center;
        public float height;
        public float radius;
    }

    [Header("First Player Settings")]
    [SerializeField] private ControllerSettings firstPersonSettings;

    [Header("Third Player Settings")]
    [SerializeField] private ControllerSettings thirdPersonSettings;

    [SerializeField] private List<string> firstPersonScenes;

    private CharacterController _controller;
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _controller = GetComponent<CharacterController>();

        if (_controller == null)
            Debug.LogError("CharacterController não encontrado no objeto!");
        if (_playerController == null)
            Debug.LogError("PlayerController não encontrado no objeto!");
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Determina o estado do jogador (primeira ou terceira pessoa)
        _playerController._isThirdPlayer = !firstPersonScenes.Contains(scene.name);
        var settings = _playerController._isThirdPlayer ? thirdPersonSettings : firstPersonSettings;

        // Atualiza as configurações do CharacterController
        UpdateCharacterController(settings);

        // Alterna entre os modelos e câmaras
        TogglePlayerView(settings, scene.name);

        // Posiciona o jogador no SpawnPoint da cena
        PositionPlayerAtSpawnPoint(scene.name);
    }

    private void UpdateCharacterController(ControllerSettings settings)
    {
        _controller.center = settings.center;
        _controller.height = settings.height;
        _controller.radius = settings.radius;

        Debug.Log($"Controller atualizado para: Center={_controller.center}, Height={_controller.height}, Radius={_controller.radius}");
    }

    private void TogglePlayerView(ControllerSettings settings, string sceneName)
    {
        thirdPersonSettings.mesh.SetActive(_playerController._isThirdPlayer);
        firstPersonSettings.mesh.SetActive(!_playerController._isThirdPlayer);

        Debug.Log("scene name: " + sceneName);
        Debug.Log("Faz o que quero? " + sceneName.EndsWith("Cutscene"));
        if (sceneName.EndsWith("Cutscene"))
        {
            thirdPersonSettings.camera.gameObject.SetActive(false);
            firstPersonSettings.camera.gameObject.SetActive(false);
            return;
        }

        thirdPersonSettings.camera.gameObject.SetActive(_playerController._isThirdPlayer);
        firstPersonSettings.camera.gameObject.SetActive(!_playerController._isThirdPlayer);
    }

    private void PositionPlayerAtSpawnPoint(string sceneName)
    {
        Transform spawnPoint = GameObject.Find("SpawnPoint")?.transform;

        if (spawnPoint != null)
        {
            _controller.enabled = false; // Desativa temporariamente para evitar bugs de colisão
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
            _controller.enabled = true; // Reativa o CharacterController

            Debug.Log($"Player posicionado no SpawnPoint: {spawnPoint.position}");
        }
        else
        {
            Debug.LogWarning($"SpawnPoint não encontrado na cena {sceneName}. Usando posição atual.");
        }
    }

}
