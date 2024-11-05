using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialJumping : TutorialStep
{
    private bool tutorialHasFinished = false;
    private bool pressedSpace;

    public override void ActivateStep()
    {
        this.gameObject.SetActive(true);
        tutorialText.text = message;
        pressedSpace = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) pressedSpace = true;

        if (pressedSpace && !tutorialHasFinished)
        {
            tutorialManager.CompleteStep();
        }
    }

    public override void DeactivateStep()
    {
        Debug.Log("Consegui saltar!");
        tutorialHasFinished = true;
        this.gameObject.SetActive(false);
    }
}
