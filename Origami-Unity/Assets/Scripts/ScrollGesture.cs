using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class ScrollGesture : MonoBehaviour, IMixedRealityGestureHandler<Vector3>
{
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
            Vector3 inputDir = gameObject.transform.rotation * eventData.InputData;
            gameObject.transform.Rotate(new Vector3(0f, 0f, (inputDir.x - inputDir.z) * rotationSpeed));
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
}
