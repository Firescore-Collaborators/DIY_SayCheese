using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStepState : MonoBehaviour
{
    public GameObject stepManager;
    public virtual void OnStepStart()
    {
        UIElements.instance.StepStart();
        stepManager.SetActive(true);
    }

    public virtual void OnStepEnd()
    {
        stepManager.SetActive(false);
        UIElements.instance.StepComplete();
    }
}
