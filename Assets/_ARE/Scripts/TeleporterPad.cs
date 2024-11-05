using UnityEngine;

public class TeleporterPad : MonoBehaviour
{
    [Header("Teleporter Settings")]
    [SerializeField] private Transform destination;
    bool hasLeftCube = false;
    bool jumpedOnPlatform = false;

    [Header("Objects To Enable")]
    [SerializeField] private GameObject objectToEnable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hasLeftCube && jumpedOnPlatform)
        {
            objectToEnable.SetActive(true);

            if (other.transform != null &&
                other.transform.TryGetComponent<PlayerController>(out var player))
            {
                player.Teleport(destination.position, destination.rotation);
            }
            hasLeftCube = false;
            jumpedOnPlatform = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<PlayerState>(out var player) &&
                player.CurrentPlayerMovementState == PlayerMovementState.Jumping)
            {
                jumpedOnPlatform = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasLeftCube = true;
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
