﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpObjectRotation : MonoBehaviour
{
    public static LerpObjectRotation instance;
    public bool toLerp = false;
    float lerpSpeed;
    public float lerpTime;

    Quaternion initRot;
    Quaternion finalRot;
    Transform currentObject;
    System.Action lerpComplete;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Update()
    {
        if (toLerp == false)
            return;
        
        currentObject.transform.rotation = Quaternion.Lerp(initRot, finalRot, lerpTime);

        if (lerpTime < 1.0f)
        {
            lerpTime += Time.deltaTime / lerpSpeed;
        }
        else
        {
            toLerp = false;
            lerpTime = 0;
            if (lerpComplete != null)
            {
                lerpComplete.Invoke();
            }
        }

    }
    public void LerpObject(Transform lerpObject, Quaternion _finalRot, float speed, System.Action callback = null, bool startLerpAuto = true)
    {
        currentObject = lerpObject;
        initRot = currentObject.transform.rotation;
        finalRot = _finalRot;
        lerpSpeed = speed;
        lerpTime = 0;
        if (callback != null)
            lerpComplete = callback;
        else
            lerpComplete = null;

        if (startLerpAuto)
            toLerp = true;
    }
}
