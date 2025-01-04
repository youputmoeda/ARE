using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFinishTutorial : TutorialStep
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
            tutorialManager.CompleteStep(5f);
        }
    }

    public override void DeactivateStep()
    {
        Debug.Log("Acabou a primeira parte!");
        tutorialHasFinished = true;
    }
}
