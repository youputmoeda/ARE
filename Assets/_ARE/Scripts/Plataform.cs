using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.CompareTag("Player"))
            other.GetComponent<Transform>().SetParent(gameObject.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Stay");
        if (other.CompareTag("Player"))
            other.GetComponent<Transform>().SetParent(gameObject.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Transform>().SetParent(null);
            DontDestroyOnLoad(other.gameObject);
        }
    }
}
