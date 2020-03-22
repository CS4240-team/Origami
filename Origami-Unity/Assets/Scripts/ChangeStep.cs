﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class ChangeStep : MonoBehaviour
{
    private int step;
    public TextMeshPro step_t;
    public Interactable leftArrow;
    public Interactable rightArrow;
    // Start is called before the first frame update
    void Start()
    {
        step = 1;
        if (step == 1)
            leftArrow.IsEnabled = false;
        else if (step == 16)
            rightArrow.IsEnabled = false;
    }

    public void changeStep(int n)
    {
        step += n;
        if (step == 1)
            leftArrow.IsEnabled = false;
        else if (step == 16)
            rightArrow.IsEnabled = false;
        else if (!leftArrow.IsEnabled)
            leftArrow.IsEnabled = true;
        else if (!rightArrow.IsEnabled)
            rightArrow.IsEnabled = true;
        step_t.text = step.ToString() + " / 16";
    }
}