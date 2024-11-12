using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBeforeDestroyEnemy : TutorialStep
{
    private bool tutorialHasFinished = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !tutorialHasFinished)
        {
            tutorialText.text = message;
            tutorialHasFinished = true;
            Invoke(nameof(EndThisTutorialWithTime), 2f);
        }
    }

    private void EndThisTutorialWithTime()
    {
        tutorialManager.CompleteStep(0f);
    }

    public override void ActivateStep()
    {
        tutorialHasFinished = false;
    }

    public override void DeactivateStep()
    {
        Debug.Log("Start enemy tutorial!");
    }
}
