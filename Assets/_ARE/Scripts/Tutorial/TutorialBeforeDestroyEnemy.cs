using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBeforeDestroyEnemy : TutorialStep
{
    [SerializeField] private Animator _lastDoorAnimator;
    private bool tutorialHasFinished = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !tutorialHasFinished)
        {
            tutorialText.text = message;
            tutorialHasFinished = true;
            _lastDoorAnimator.SetTrigger("start");
            Invoke(nameof(EndThisTutorialWithTime), 4f);
        }
    }

    private void EndThisTutorialWithTime()
    {
        tutorialManager.CompleteStep(0f);
    }

    public override void ActivateStep()
    {
        tutorialHasFinished = false;
    }

    public override void DeactivateStep()
    {
        Debug.Log("Start enemy tutorial!");
    }
}
