using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public List<GameObject> objectsToEnable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (objectsToEnable != null)
            {
                objectsToEnable.ForEach(obj => obj.SetActive(true));
            }

            Destroy(gameObject);
        }
    }
}
