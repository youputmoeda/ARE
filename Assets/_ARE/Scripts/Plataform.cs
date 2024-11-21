using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<Transform>().SetParent(gameObject.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<Transform>().SetParent(gameObject.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<Transform>().SetParent(null);
    }
}
