using UnityEngine;
using System.Collections;

public class TutorialWelcomeLastTutorial : TutorialStep
{

    public override void ActivateStep()
    {
        tutorialText.text = message;
        Invoke(nameof(EndThisTutorialWithTime), 5f);
    }

    public override void DeactivateStep()
    {
        Debug.Log("Tutorial started.");
    }

    private void EndThisTutorialWithTime()
    {
        tutorialManager.CompleteStep();
    }
}
