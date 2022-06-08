using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StepStateMachine 
{
    
    public static Dictionary<StepType,string> stepStates = new Dictionary<StepType, string>{
        {StepType.dirtClean, "DirtCleanState"},
        {StepType.painting, "PaintStepState"},
        {StepType.sticker, "StickerStepState"},
        {StepType.levelEnd, "LevelCompleteStepState"},
        {StepType.stencil, "StencilStepState"},
        {StepType.stamp, "StampStepState"},
        {StepType.moneyCut, "MoneyCutStepState"},
    };


}
