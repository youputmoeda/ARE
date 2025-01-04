using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private (Vector3 position, Quaternion rotation) newPos;

    private void Start()
    {
        Collider collider = gameObject.GetComponent<Collider>();

        if (collider != null)
        {
            if (collider is BoxCollider boxCollider)
            {
                newPos.position = transform.position + transform.TransformVector(boxCollider.center);
            }
            else
            {
                newPos.position = transform.position;
            }
            newPos.rotation = transform.rotation;
        }
        else
        {
            Debug.LogWarning("Nenhum Collider foi encontrado no GameObject.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController._initialPositionAndRotation = newPos;
                Debug.Log($"Posição salva: {newPos.position}, Rotação salva: {newPos.rotation}");
            }
            else
            {
                Debug.LogWarning("O objeto 'Player' não possui o componente PlayerController.");
            }
        }
    }
}
