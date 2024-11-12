using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRightMouse : TutorialStep
{

    private bool pressedRightMouse;
    private bool tutorialHasFinished = false;

    public override void ActivateStep()
    {
        this.gameObject.SetActive(true);
        tutorialText.text = message;
        pressedRightMouse = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) pressedRightMouse = true;

        if (pressedRightMouse && !tutorialHasFinished)
        {
            tutorialManager.CompleteStep();
        }
    }

    public override void DeactivateStep()
    {
        Debug.Log("Passo de movimento concluído.");
        tutorialHasFinished = true;
        this.gameObject.SetActive(true);
    }
}
