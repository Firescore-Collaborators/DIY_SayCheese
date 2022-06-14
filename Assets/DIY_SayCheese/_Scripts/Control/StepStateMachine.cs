using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StepStateMachine 
{
    
    public static Dictionary<StepType,string> stepStates = new Dictionary<StepType, string>{
        {StepType.cutting, "CuttingStepState"},
        {StepType.levelEnd, "LevelCompleteStepState"},
        {StepType.plate, "CheesePlateStepState"},
        {StepType.grate, "CheeseGrateStepState"},
    };


}
