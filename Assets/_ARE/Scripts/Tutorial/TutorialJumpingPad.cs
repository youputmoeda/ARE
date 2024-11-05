using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialJumpingPad : TutorialStep
{
    bool tutorialHasFinished = false;
    bool hasLeftCube;

    public override void ActivateStep()
    {
        tutorialText.text = message;
        hasLeftCube = false;
        this.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hasLeftCube && !tutorialHasFinished)
        {
            hasLeftCube = false;
            tutorialManager.CompleteStep();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasLeftCube = true;
        }
    }

    public override void DeactivateStep()
    {
        Debug.Log("Pressinou no sítio certo");
        tutorialHasFinished = true;
    }
}
