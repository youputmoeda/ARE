using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveAmmo : MonoBehaviour
{
    public int _ammoToGive;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShoot _shootSystem = other.GetComponent<PlayerShoot>();
            _shootSystem.GiveAmmo(_ammoToGive);
        }
    }
}
