using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class LevelSO : ScriptableObject
{
    public GameObject levelObject;
    public LevelType levelType;

    public List<StepSO> steps = new List<StepSO>();

    public System.Action Start;
    public System.Action Reset;

    public virtual void OnLevelStart()
    {
        Start?.Invoke();
    }

    public virtual void OnLevelEnd()
    {
        Reset?.Invoke();
        UIElements.instance.LevelComplete();
    }

}
