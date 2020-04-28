using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System;

public class MainMenuController : MonoBehaviour
{
    public GameObject origamiParent;
    public UIController uiController;

    private BoundingBox[] origamiBoxes;
    private List<Vector3> initPositions = new List<Vector3>();
    private List<Quaternion> initRotations = new List<Quaternion>();
    private bool boxesState;
    // Start is called before the first frame update
    void Awake()
    {
        origamiBoxes = origamiParent.GetComponentsInChildren<BoundingBox>();
        for (int i = 0; i < origamiBoxes.Length; i++)
        {
            initPositions.Add(origamiBoxes[i].transform.position);
            initRotations.Add(origamiBoxes[i].transform.rotation);
        }
    }
    private void OnEnable()
    {
        boxesState = false;
        for (int i = 0; i < origamiBoxes.Length; i++)
        {
            origamiBoxes[i].Active = false;
            origamiBoxes[i].transform.localScale = new Vector3(1, 1, 1);
            origamiBoxes[i].transform.rotation = initRotations[i];
            origamiBoxes[i].transform.position = initPositions[i];
        }
        uiController.setCurrentMenu(1);
    }

    public void activateBoundingBox()
    {
        for (int i = 0; i < origamiBoxes.Length; i++)
        {
            if (!origamiBoxes[i].Active)
            {
                origamiBoxes[i].Active = true;
            }
            else
            {
                origamiBoxes[i].Active = false;
                origamiBoxes[i].transform.localScale = new Vector3(1, 1, 1);
                origamiBoxes[i].transform.rotation = initRotations[i];
                origamiBoxes[i].transform.position = initPositions[i];
                
            }
        }
        if (origamiBoxes[0].Active)
            boxesState = true;
        else
            boxesState = false;
    }

    public bool getBoxesState()
    {
        return boxesState;
    }
}
