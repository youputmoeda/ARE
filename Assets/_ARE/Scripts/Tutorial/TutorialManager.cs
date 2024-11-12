using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private List<TutorialStep> tutorialSteps;
    [SerializeField] private string lastMessage;
    private int currentStepIndex = 0;

    private void Start()
    {
        ShowCurrentStep();
    }

    private void ShowCurrentStep()
    {
        if (currentStepIndex < tutorialSteps.Count)
        {
            tutorialSteps[currentStepIndex].Initialize(this, tutorialText);
            tutorialSteps[currentStepIndex].ActivateStep();
        }
    }

    public void CompleteStep(float nextStep = 2f)
    {
        if (currentStepIndex < tutorialSteps.Count)
        {
            tutorialText.text = tutorialSteps[currentStepIndex].nextMessage;
            tutorialSteps[currentStepIndex].DeactivateStep();
            Invoke(nameof(ShowNextStep), nextStep);
        }
    }

    private void ShowNextStep()
    {
        currentStepIndex++;
        

        if (currentStepIndex < tutorialSteps.Count)
        {
            ShowCurrentStep(); // Mostra o próximo passo
        }
        else
        {
            tutorialText.text = lastMessage;
        }
    }
}