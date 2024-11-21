using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Item variables

    // Door open animations

    public Interact openFromInteraction;

    private void OnEnable()
    {
        Interact check = GetComponent<Interact>();
        if (check)
        {
            openFromInteraction = check;
            openFromInteraction.GetInteractEvent.HasInteracted += OpenDoor;
        }
        else
        {
            Interact addComp = gameObject.AddComponent<Interact>();
            openFromInteraction = addComp;
            openFromInteraction.GetInteractEvent.HasInteracted += OpenDoor;
        }
    }

    private void OnDisable()
    {
        if (openFromInteraction)
        {
            openFromInteraction.GetInteractEvent.HasInteracted -= OpenDoor;
        }
    }

    public void OpenDoor()
    {
        Debug.Log("Door is now open.");
    }
}


