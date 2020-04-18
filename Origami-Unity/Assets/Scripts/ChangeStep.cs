using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class ChangeStep : MonoBehaviour
{
    private int step;
    private Animator origami_anim;

    public TextMeshPro step_t;
    public Interactable leftArrow;
    public Interactable rightArrow;
    public GameObject origami;
    // Start is called before the first frame update
    void Start()
    {
        step = 1;
        if (step == 1)
            leftArrow.IsEnabled = false;
        else if (step == 16)
            rightArrow.IsEnabled = false;

        origami_anim = origami.GetComponent<Animator>();
    }

    public void changeStep(int n)
    {
        if (step + n < 1)
            leftArrow.IsEnabled = false;
        else if (step + n > 16)
            rightArrow.IsEnabled = false;
        else
        {
            if (n == -1)
                origami_anim.SetTrigger("back");
            else if (n == 1)
                origami_anim.SetTrigger("next");
            else if (n == 0)
                origami_anim.SetTrigger("repeat");
            step += n;
            step_t.text = step.ToString() + " / 16";

            if (!leftArrow.IsEnabled)
                leftArrow.IsEnabled = true;
            else if (!rightArrow.IsEnabled)
                rightArrow.IsEnabled = true;
        }
    }
}
