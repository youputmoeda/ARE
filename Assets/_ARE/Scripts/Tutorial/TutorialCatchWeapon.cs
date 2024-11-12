using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCatchWeapon : TutorialStep
{
    private bool tutorialHasFinished = false;

    public override void ActivateStep()
    {
        tutorialText.text = message;
        Invoke(nameof(CleanMessage), 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !tutorialHasFinished)
        {
            other.gameObject.GetComponent<PlayerShoot>().enabled = true;

            tutorialManager.CompleteStep(4f);

            tutorialHasFinished = true;
        }
    }

    private void CleanMessage()
    {
        tutorialText.text = "";
    }

    public override void DeactivateStep()
    {
        Debug.Log("Apanhou a arma!");
    }
}
