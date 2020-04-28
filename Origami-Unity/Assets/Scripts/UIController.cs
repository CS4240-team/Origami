using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject[] menus;
    public GameObject setupPlane;
    public GameObject plane;

    private int currentStep = 1;
    private int currentMenu = 0;    // 0 = setUp, 1 = main, 2 = instruction, 3 = steps
    private string origami = "";

    public void displayMainMenu()
    {
        //resize rotate and change position of plane to the same as setup plane
        if (currentMenu == 0)
        {
            Transform setupTransform = setupPlane.transform;
            plane.transform.position = setupTransform.position;
            plane.transform.localScale = new Vector3(setupTransform.localScale.x, (setupTransform.localScale.x + setupTransform.localScale.z)/2, setupTransform.localScale.z) / 10;
            plane.transform.rotation = setupTransform.rotation;
            plane.SetActive(true);
        }

        menus[currentMenu].SetActive(false);
        menus[1].SetActive(true);
    }

    public void displayInstructionMenu(string origami_name = "")
    {
        if(!origami_name.Equals(""))
            origami = origami_name.ToLower();

        menus[currentMenu].SetActive(false);
        menus[2].SetActive(true);
    }

    public void displayStepSelectionScreen()
    {
        menus[currentMenu].SetActive(false);
        menus[3].SetActive(true);
    }

    public string getOrigami()
    {
        return origami;
    }

    public int getCurrentStep()
    {
        return currentStep;
    }

    public int getStepsTotal(string origami_name)
    {
        if (origami_name.Equals("crane"))
            return 16;
        return 10;
    }

    public void setCurrentMenu(int menuNo)
    {
        currentMenu = menuNo;
    }

    public void setCurrentStep(int stepNo)
    {
        currentStep = stepNo;
    }
}
