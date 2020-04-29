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
        public GameObject crane;
        public UIController uiController;
        public Theme buttonTheme;
        public Theme currentButtonTheme;
        public States buttonsStates;
        public TextMeshPro circleText;

        private readonly float menuRadius = -13.2f;
        private readonly float angle = 7f;
        private string origami = "";
        private GameObject origami_model;
        private int currentStep;
        private int stepsTotal;

        private void OnEnable()
        {
            Debug.Log("Switching to Steps Menu");
            //Instantiate origami model on the inner circle
            if (!origami.Equals(uiController.getOrigami()))
            {
                var plane = transform.root;
                if (origami_model != null)
                    Destroy(origami_model);
                origami_model = Instantiate(crane, stepsMenu.transform);
                origami_model.transform.localPosition = new Vector3(3.75f, 0, 3);
            } 
            origami = uiController.getOrigami();
            currentStep = uiController.getCurrentStep();
            stepsTotal = uiController.getStepsTotal(origami);

            //Change inner circle text
            if(!origami.Equals(""))
                circleText.text = $"{origami.ToUpper().Substring(0,1) + origami.Substring(1)}\n\nCurrent step: \n{currentStep} / {stepsTotal}";

            populateStepsMenu(currentStep, stepsTotal);
            stepsMenu.SetActive(true);
            uiController.setCurrentMenu(3);
        }

        public void selectStep(int stepNo)
        {
            Debug.Log($"Step {stepNo} selected");
            unpopulateMenu();
            stepsMenu.SetActive(false);
            uiController.setCurrentStep(stepNo);
            uiController.displayInstructionMenu();
        }

        private void RotateMenu(float deg)
        {
            stepMenuCircle.transform.localRotation = Quaternion.Euler(0, 0, -deg);
        }

        public float getAngle()
        {
            return angle;
        }

        private void populateStepsMenu(int current, int total)
        {
            Vector3 parentGlobalScale = stepMenuCircle.transform.lossyScale;
            Vector3 parentPosition = stepMenuCircle.transform.position;
            Quaternion parentRotation = stepMenuCircle.transform.rotation;

            for (int x = 0; x < total; x++)
            {
                //Create button
                Vector3 pos = new Vector3(Mathf.Cos(x * angle * Mathf.Deg2Rad) * menuRadius * parentGlobalScale.x,
                                          Mathf.Sin(x * angle * Mathf.Deg2Rad) * menuRadius * parentGlobalScale.y,
                                          0);
                Quaternion rot = Quaternion.Euler(0f, 0f, x * angle);
                pos = parentRotation * pos;
                GameObject buttonInstance = Instantiate(stepButtonPrefab, parentPosition + pos, parentRotation * rot, stepMenuCircle.transform);

                buttonInstance.name = $"button {x + 1}";

                //Assign instruction image to buttons
                var img = Resources.Load<Sprite>($"{origami}/{origami}{x+1}");
                var imgComponent = buttonInstance.transform.Find("Image").GetComponent<Image>();
                imgComponent.sprite = img;

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
                Theme theme;
                interactable.States = buttonsStates;
                if (x == current - 1)
                    theme = currentButtonTheme;
                else
                    theme = buttonTheme;
                interactable.Profiles = new List<InteractableProfileItem>()
                {
                    new InteractableProfileItem()
                    {
                        Themes = new List<Theme>() { theme },
                        Target = buttonInstance
                    }
                };

                interactable.OnClick.AddListener(() => this.selectStep(int.Parse(buttonInstance.name.Substring(7))));

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
