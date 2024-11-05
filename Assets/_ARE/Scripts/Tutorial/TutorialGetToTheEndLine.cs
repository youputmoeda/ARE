using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGetToTheEndLine : TutorialStep
{
    private bool tutorialHasFinished = false;

    public override void ActivateStep()
    {
        this.gameObject.SetActive(true);
        tutorialText.text = message;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !tutorialHasFinished)
        {
            tutorialManager.CompleteStep(5f);

        }
    }

    public override void DeactivateStep()
    {
        Debug.Log("Pass The LastDoor");
        tutorialHasFinished = true;
    }
}
