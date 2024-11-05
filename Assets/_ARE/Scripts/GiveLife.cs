using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveLife : MonoBehaviour
{
    public int lifeToRecover;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LifeSystem _lifeSystem = other.GetComponent<LifeSystem>();
            _lifeSystem.RecoverLife(lifeToRecover);
        }
    }
}
