using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Header("Teleporter Settings")]
    public Transform destination;
    public bool teleportByTrigger = true;
    private GameObject _player;

    private void Start()
    {
        if (!teleportByTrigger)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void Teleportation()
    {
        if (_player != null && _player.TryGetComponent<PlayerController>(out var player))
        {
            player.Teleport(destination.position, destination.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (teleportByTrigger && other.CompareTag("Player"))
        {
            if (other.transform != null && other.transform.TryGetComponent<PlayerController>(out var player))
                player.Teleport(destination.position, destination.rotation);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(destination.position, .4f);
        var direction = destination.TransformDirection(Vector3.forward);
        Gizmos.DrawRay(destination.position, direction);
    }
}
