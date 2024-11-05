using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class TutorialStep : MonoBehaviour
{
    public string message;
    public string nextMessage;

    protected TutorialManager tutorialManager;
    protected TMP_Text tutorialText;

    public void Initialize(TutorialManager manager, TMP_Text text)
    {
        tutorialManager = manager;
        tutorialText = text;
    }

    public abstract void ActivateStep();
    public abstract void DeactivateStep();
}
