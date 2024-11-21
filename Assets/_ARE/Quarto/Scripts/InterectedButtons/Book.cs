using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Book : MonoBehaviour
{
    // Item variables

    // Picture open animations

    public Interact openFromInteraction;

    private void OnEnable()
    {
        Interact check = GetComponent<Interact>();
        if (check)
        {
            openFromInteraction = check;
            openFromInteraction.GetInteractEvent.HasInteracted += OpenBook;
        }
        else
        {
            Interact addComp = gameObject.AddComponent<Interact>();
            openFromInteraction = addComp;
            openFromInteraction.GetInteractEvent.HasInteracted += OpenBook;
        }
    }

    private void OnDisable()
    {
        if (openFromInteraction)
        {
            openFromInteraction.GetInteractEvent.HasInteracted -= OpenBook;
        }
    }

    public void OpenBook()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Book is now open.");
    }
}

