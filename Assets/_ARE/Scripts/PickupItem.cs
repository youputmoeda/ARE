using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public List<GameObject> objectsToEnable;

    [SerializeField] private AudioClip pickingSoundClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (objectsToEnable != null)
            {
                SoundFXManager.instance.PlaySoundFXClip(pickingSoundClip, transform, 1f);
                objectsToEnable.ForEach(obj => obj.SetActive(true));
            }

            Destroy(this.gameObject);
        }
    }
}
