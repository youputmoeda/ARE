using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCatchLife : TutorialStep
{
    private bool tutorialHasFinished = false;

    public override void ActivateStep()
    {
        tutorialText.text = message;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !tutorialHasFinished)
        {
            tutorialManager.CompleteStep();
            tutorialHasFinished = true;
        }
    }

    public override void DeactivateStep()
    {
        Debug.Log("Apanhou a vida");
    }
}
