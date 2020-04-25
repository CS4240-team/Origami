using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ActivateBoundingBoxScript : MonoBehaviour
{
    public GameObject origamiParent;
    private BoundingBox[] origamiBoxes;
    private List<Vector3> initPositions = new List<Vector3>();
    private List<Quaternion> initRotations = new List<Quaternion>();
    // Start is called before the first frame update
    void Start()
    {
        origamiBoxes = origamiParent.GetComponentsInChildren<BoundingBox>();
        Debug.Log(origamiBoxes[0].name);
    }

    public void activateBoundingBox()
    {
        for(int i=0;i<origamiBoxes.Length;i++)
        {
            if (!origamiBoxes[i].Active)
            {
                origamiBoxes[i].Active = true;
                initPositions.Add(origamiBoxes[i].transform.position);
                initRotations.Add(origamiBoxes[i].transform.rotation);
            }
            else
            {
                origamiBoxes[i].Active = false;
                origamiBoxes[i].transform.localScale = new Vector3(1, 1, 1);
                origamiBoxes[i].transform.rotation = initRotations[i];
                origamiBoxes[i].transform.position = initPositions[i];
                if (i == origamiBoxes.Length - 1)
                {
                    initPositions.Clear();
                    initRotations.Clear();
                }
            }
        }
    }
}
