using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public enum StepType 
{
    cutting = 0,
    levelEnd = 1,
    plate = 2,
}
public class StepSO : ScriptableObject
{

    public StepType stepType;

    //[Dropdown("CameraValue")]
    public string cameraName;
    public int blendSpeed;

    //private List<string> CameraValue { get { return new List<string>() { "Default", "TopDown","Empty","Sticker"}; } }

    public virtual void OnStepStart()
    {
        if(cameraName.Equals( "Empty"))
        {
            return;
        }
        Timer.Delay(0.1f, () =>
        {
            CameraController.instance.SetCurrentCamera(cameraName,blendSpeed);
        });
    }

    public virtual void OnStepEnd()
    {
        Debug.Log("OnStepEnd");
    }
}
