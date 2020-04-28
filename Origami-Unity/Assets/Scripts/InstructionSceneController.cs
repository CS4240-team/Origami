using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;
using System;

public class InstructionSceneController : MonoBehaviour
{
    private int step;
    private int stepTotal;
    private Animator origamiAnimator;

    public TextMeshPro stepLabel;
    public Interactable leftArrow;
    public Interactable rightArrow;
    public GameObject origami;
    public UIController uiController;

    private void OnEnable()
    {
        step = 1;
        string origami_name = uiController.getOrigami();
        stepTotal = uiController.getStepsTotal(origami_name);
        origamiAnimator = origami.GetComponent<Animator>();
        setStep(uiController.getCurrentStep());
        uiController.setCurrentMenu(2);
    }

    private void OnDisable()
    {
        uiController.setCurrentStep(step);
    }

    public void changeStep(int n)
    {
        setStep(step + n);
    }

    private void setStep(int s)
    {
        if (s == step)
            origamiAnimator.SetTrigger("repeat");
        else
        {
            step = s;
            origamiAnimator.Play($"Step{step}");
            stepLabel.text = $"{step} / {stepTotal}";

            if (step >= stepTotal)
                rightArrow.IsEnabled = false;
            else if (step <= 1)
                leftArrow.IsEnabled = false;
            else
            {
                leftArrow.IsEnabled = true;
                rightArrow.IsEnabled = true;
            }
        }
    }
}
