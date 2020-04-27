using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;

public class ScrollGesture : MonoBehaviour, IMixedRealityGestureHandler<Vector3>
{
    public StepMenuController stepmenuScript;

    public float rotationSpeed = 1;

    public void OnGestureStarted(InputEventData eventData)
    {
        string inputActionType = eventData.MixedRealityInputAction.Description;
        Debug.Log($"{inputActionType} Gesture started for {gameObject.name}");

        if (inputActionType == "Navigation Action")
        {
            gameObject.transform.localScale = new Vector3(1.02f, 1.02f, 1f);
        }
    }

    public void OnGestureUpdated(InputEventData eventData)
    {
        string inputActionType = eventData.MixedRealityInputAction.Description;
        Debug.Log($"{inputActionType} Gesture updated for {gameObject.name}. No Action Taken");
    }

    public void OnGestureUpdated(InputEventData<Vector3> eventData)
    {
        string inputActionType = eventData.MixedRealityInputAction.Description;
        Debug.Log($"{inputActionType} Gesture updated for {gameObject.name}: {eventData.InputData}");

        if (inputActionType == "Navigation Action")
        {
            Vector3 inputDir = eventData.InputData;
            float z = (inputDir.x + inputDir.y) * rotationSpeed;
            z = ClampAngle(z, (stepmenuScript.getStepTotal("crane") - 1) * stepmenuScript.getAngle() * -1, 0f);

            transform.Rotate(new Vector3(0,0,z));
        }
    }

    public void OnGestureCompleted(InputEventData eventData)
    {
        string inputActionType = eventData.MixedRealityInputAction.Description;
        Debug.Log($"{inputActionType} Gesture completed for {gameObject.name}. No action taken");
    }

    public void OnGestureCompleted(InputEventData<Vector3> eventData)
    {
        string inputActionType = eventData.MixedRealityInputAction.Description;
        Debug.Log($"{inputActionType} Gesture completed for {gameObject.name}");

        if (inputActionType == "Navigation Action")
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void OnGestureCanceled(InputEventData eventData)
    {
        string inputActionType = eventData.MixedRealityInputAction.Description;
        Debug.Log($"{inputActionType} Gesture cancelled for {gameObject.name}");

        if (inputActionType == "Navigation Action")
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }
}
