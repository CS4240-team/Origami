using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject[] menus;

    private int currentStep = 0;
    private int currentMenu = 0;    // 0 = setUp, 1 = main, 2 = instruction, 3 = steps

    public void displayInstructionMenu(int stepNo)
    {
        menus[currentMenu].SetActive(false);
        menus[2].SetActive(true);
    }

    public int getCurrentStep()
    {
        return currentStep;
    }
}
