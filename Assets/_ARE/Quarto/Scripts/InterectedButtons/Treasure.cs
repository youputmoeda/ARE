using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    // Item variables
    [SerializeField] List<Animator> _animations = null;
    [SerializeField] GameObject _itemToEnable = null;
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
        if (_animations != null)
            _animations.ForEach(x => x.SetTrigger("start"));

        if (_itemToEnable != null)
            _itemToEnable.SetActive(true);
    }
}

