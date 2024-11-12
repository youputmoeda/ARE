using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSprintStep : TutorialStep
{
    public GameObject doorToEnable;

    private bool pressedL_Sfhit;
    private bool tutorialHasFinished = false;

    public override void ActivateStep()
    {
        tutorialText.text = message;
        this.gameObject.SetActive(true);
        pressedL_Sfhit = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) pressedL_Sfhit = true;

        if (pressedL_Sfhit && !tutorialHasFinished)
        {
            tutorialManager.CompleteStep();
        }
    }

    public override void DeactivateStep()
    {
        Debug.Log("Passo de sprint concluído.");
        doorToEnable.SetActive(true);
        tutorialHasFinished = true;
        this.gameObject.SetActive(false);
    }
}
