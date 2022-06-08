using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public enum StepType 
{
    dirtClean = 0,
    stamp = 1,
    painting = 2,
    stencil = 3,
    sticker = 4,
    moneyCut = 5,
    levelEnd = 6,
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
