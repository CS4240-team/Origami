using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Microsoft.MixedReality.Toolkit.UI
{
    public class StepMenuController : MonoBehaviour
    {
        public GameObject stepsMenu;
        public GameObject stepButtonPrefab;
        public GameObject stepMenuCircle;
        public UIController uiController;
        public Theme buttonTheme;
        public States buttonsStates;

        private readonly float menuRadius = -1.32f;
        private readonly float angle = 7f;

        private void OnEnable()
        {
            Debug.Log("Switching to Steps Menu");
            string origami = uiController.getOrigami();
            int currentStep = uiController.getCurrentStep();
            int stepsTotal = uiController.getStepsTotal(origami);
            populateStepsMenu(currentStep, stepsTotal);
            stepsMenu.SetActive(true);
            uiController.setCurrentMenu(3);
        }

        public void selectStep(int stepNo)
        {
            Debug.Log($"Step {stepNo} selected");
            unpopulateMenu();
            stepsMenu.SetActive(false);
            uiController.displayInstructionMenu(stepNo);
        }

        private void RotateMenu(float deg)
        {
            stepMenuCircle.transform.Rotate(0, 0, -deg);
        }

        public float getAngle()
        {
            return angle;
        }

        private void populateStepsMenu(int current, int total)
        {
            for (int x = 0; x < total; x++)
            {
                //Create button
                Vector3 pos = new Vector3(Mathf.Cos(x * angle * Mathf.Deg2Rad) * menuRadius, Mathf.Sin(x * angle * Mathf.Deg2Rad) * menuRadius, 0);
                Quaternion rot = Quaternion.Euler(0f, 0f, x * angle);
                pos = stepMenuCircle.transform.rotation * pos;
                GameObject buttonInstance = Instantiate(stepButtonPrefab, stepMenuCircle.transform.position + pos, stepMenuCircle.transform.rotation * rot, stepMenuCircle.transform);
                buttonInstance.name = $"button {x + 1}";

                //Change number accordingly
                GameObject grandchild = buttonInstance.transform.GetChild(0).GetChild(0).gameObject;
                if (grandchild.name == "Number")
                {
                    TMP_Text stepNo = grandchild.GetComponent<TMP_Text>();
                    stepNo.text = (x + 1).ToString();
                }
                else
                {
                    Debug.LogError("Fail to find Number grandchild of Button Prefab");
                }

                //Add interable component
                Interactable interactable = buttonInstance.AddComponent<Interactable>();
                interactable.States = buttonsStates;
                interactable.Profiles = new List<InteractableProfileItem>()
                {
                    new InteractableProfileItem()
                    {
                        Themes = new List<Theme>() { buttonTheme },
                        Target = buttonInstance
                    }
                };

                interactable.OnClick.AddListener(() => this.selectStep(int.Parse(buttonInstance.name.Substring(7))));

                //Disable the currently chosen step
                if (x == current - 1)
                {
                    interactable.IsEnabled = false;
                }

            }
            RotateMenu((current - 1) * angle);
        }

        private void unpopulateMenu()
        {
            int childCount = stepMenuCircle.transform.childCount;
            for (int x = 0; x < childCount; x++)
            {
                if (stepMenuCircle.transform.GetChild(x).name.Contains("Button"))
                {
                    Destroy(stepMenuCircle.transform.GetChild(x).gameObject);
                }
            }
        }
    }
}
