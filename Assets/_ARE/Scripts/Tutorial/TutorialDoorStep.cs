using UnityEngine;
using System.Collections;

public class TutorialDoorStep : TutorialStep
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
            
        }
    }

    public override void DeactivateStep()
    {
        Debug.Log("Passo da porta concluído.");
        tutorialHasFinished = true;
    }
}
