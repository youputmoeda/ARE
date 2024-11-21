using UnityEngine;

public class TeleporterPad : MonoBehaviour
{
    [Header("Teleporter Settings")]
    [SerializeField] private Transform destination;

    [Header("Objects To Enable")]
    [SerializeField] private GameObject objectToEnable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectToEnable.SetActive(true);
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
