using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Item variables
    [SerializeField] Animator selfAnimator = null;
    [SerializeField] Animator startAnimator = null;
    // Picture open animations

    public Interact openFromInteraction;

    private void OnEnable()
    {
        Interact check = GetComponent<Interact>();
        if (check)
        {
            openFromInteraction = check;
            openFromInteraction.GetInteractEvent.HasInteracted += PressedButton;
            Debug.Log("Subscribing to HasInteracted event (component already exists)");
        }
        else
        {
            Debug.Log("Adding Interact component");
            Interact addComp = gameObject.AddComponent<Interact>();
            openFromInteraction = addComp;
            openFromInteraction.GetInteractEvent.HasInteracted += PressedButton;
        }
    }

    private void OnDisable()
    {
        if (openFromInteraction)
        {
            openFromInteraction.GetInteractEvent.HasInteracted -= PressedButton;
        }
    }

    public void PressedButton()
    {
        if (selfAnimator != null)
            selfAnimator.SetTrigger("start");

        if (selfAnimator != null)
            startAnimator.SetTrigger("start");
    }
}

