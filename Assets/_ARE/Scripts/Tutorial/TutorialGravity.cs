using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGravity : TutorialStep
{
    private bool tutorialHasFinished = false;

    public override void ActivateStep()
    {
        tutorialText.text = message;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !tutorialHasFinished)
        {
            tutorialManager.CompleteStep(0f);
            tutorialHasFinished = true;
        }
    }

    public override void DeactivateStep()
    {
        Debug.Log("Consegui Mudar Gravity!");
    }
}
