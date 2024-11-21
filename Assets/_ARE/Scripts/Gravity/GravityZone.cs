using UnityEngine;
using UnityEngine.SceneManagement;

public class GravityZone : MonoBehaviour
{
    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _playerActions;
    [SerializeField] private Vector3 _newPosition;

    [SerializeField] private float _newXAngle;
    [SerializeField] private float _newYAngle;
    [SerializeField] private float _newZAngle;

    bool _isInverted = false;
    Vector3 _initialPosition;
    PlayerController _player;
    PlayerActionsInput _actionsInput;

    private void Awake()
    {
        _initialPosition = _map.transform.position;
        var teste = PlayerInputManager.Instance;
        _player = teste.GetComponentInParent<PlayerController>();
        _actionsInput = _player.GetComponent<PlayerActionsInput>();
        Debug.Log("Player encontrado: " + _player);
        Debug.Log("ActionsInput encontrado: " + _actionsInput);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        _initialPosition = _map.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("O Player está dentro do trigger da GravityZone.");
            Debug.Log("ChangeGravityPressed está ativo? " + _actionsInput.ChangeGravityPressed);

            if (_actionsInput.ChangeGravityPressed)
            {
                Debug.Log("ChangeGravityPressed ativado, alterando gravidade.");
                _map.transform.Rotate(_newXAngle, _newYAngle, _newZAngle);

                _map.transform.position = _isInverted ? _initialPosition : _newPosition;
                _isInverted = !_isInverted;
                _actionsInput.ChangeGravityPressed = !_actionsInput.ChangeGravityPressed;
                Debug.Log("Gravidade alterada. Novo estado de _isInverted: " + _isInverted);
            }
        }
    }
}
