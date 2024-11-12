using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAfterDestroyEnemy : TutorialStep
{
    [SerializeField] private List<GameObject> _objectToEnable;
    private bool tutorialHasFinished = false;

    public override void ActivateStep()
    {
        tutorialHasFinished = false;
        tutorialText.text = message;
    }

    private void OnDestroy()
    {
        if (!tutorialHasFinished)
        {
            _objectToEnable.ForEach(x => x.SetActive(true));
            tutorialManager.CompleteStep(4f);
            tutorialHasFinished = true;
        }
    }

    public override void DeactivateStep()
    {
        Debug.Log("Defeat the enemy!");
    }
}
