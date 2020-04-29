using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ProgressBar : MonoBehaviour
{
	public float speed;
	public MainMenuController mainMenuController;
	private UIController uiController;
	private GameObject barFill;

	void Start()
	{
		barFill = transform.GetChild(0).GetChild(0).gameObject;
		speed = 0;

		uiController = GameObject.Find("GlobalSceneManager").GetComponent<UIController>();
	}

	void Update()
	{
		//transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.left);
		if (speed > 0 && barFill.transform.localScale.x < 1)
		{
			barFill.transform.localScale += new Vector3(0.01f * speed, 0, 0);
		}
		else if(barFill.transform.localScale.x >= 1)
		{
			var root = transform.root;
			GameObject rootChild = null;
			//look for the name of current scene
			for(int i = 0; i < root.childCount; i++)
			{
				if (root.GetChild(i).gameObject.activeSelf)
				{
					rootChild = root.GetChild(i).gameObject;
					break;
				}
			}
			if(rootChild.name.Equals("MainMenuObjects"))
				uiController.displayInstructionMenu(origami_name: transform.parent.name.ToLower());
			else if(rootChild.name.Equals("InstructionObjects"))
			{
				if (transform.parent.name.Equals("Toblerone"))
					uiController.displayStepSelectionScreen();
				else if (transform.parent.name.Equals("Menu"))
					uiController.displayMainMenu();
			}

			barFill.transform.localScale = new Vector3(0, barFill.transform.localScale.y, barFill.transform.localScale.z);
		}

	}

	public void enlargeCanvas(bool val)
	{
		if (val && (mainMenuController == null || (mainMenuController != null && !mainMenuController.getBoxesState())))
		{
			speed = 1;
			transform.localScale = new Vector3(1, 1, 1);
		}
		else
		{
			speed = 0;
			transform.localScale = new Vector3(0, 0, 0.0001f);
			barFill.transform.localScale = new Vector3(0, 1, 1);
		}
	}
}
