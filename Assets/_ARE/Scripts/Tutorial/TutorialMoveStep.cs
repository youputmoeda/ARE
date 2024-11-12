using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMoveStep : TutorialStep
{

    private bool pressedW, pressedA, pressedS, pressedD;
    private bool tutorialHasFinished = false;

    public override void ActivateStep()
    {
        tutorialText.text = message;
        pressedW = pressedA = pressedS = pressedD = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) pressedW = true;
        if (Input.GetKeyDown(KeyCode.A)) pressedA = true;
        if (Input.GetKeyDown(KeyCode.S)) pressedS = true;
        if (Input.GetKeyDown(KeyCode.D)) pressedD = true;

        if (pressedW && pressedA && pressedS && pressedD && !tutorialHasFinished)
        {
            tutorialManager.CompleteStep();
        }
    }

    public override void DeactivateStep()
    {
        Debug.Log("Passo de movimento concluído.");
        tutorialHasFinished = true;
        this.gameObject.SetActive(false);
    }
}
