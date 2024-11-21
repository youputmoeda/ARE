using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialJumpingPad : TutorialStep
{
    bool tutorialHasFinished = false;

    public override void ActivateStep()
    {
        tutorialText.text = message;
        this.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !tutorialHasFinished)
        {
            tutorialManager.CompleteStep();
        }
    }

    public override void DeactivateStep()
    {
        Debug.Log("Pressinou no sítio certo");
        tutorialHasFinished = true;
    }
}
