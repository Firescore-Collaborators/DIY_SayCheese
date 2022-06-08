using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class LerpObjectScale : MonoBehaviour
{
    public static LerpObjectScale instance;
    bool toLerp = false;
    float lerpSpeed;
    float lerpTime;
    
    Vector3 initScale;
    Vector3 finalScale;
    Transform currentObject;

    System.Action lerpComplete;

    void Awake()
    {
        if(instance == null)
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
        if(toLerp == false)
            return;

        currentObject.transform.localScale = Vector3.Lerp(initScale,finalScale,lerpTime);

        if(lerpTime < 1.0f)
        {
            lerpTime += Time.deltaTime/lerpSpeed;
        }
        else{
            toLerp = false;
            lerpTime = 0;
            if(lerpComplete != null)
            {
                lerpComplete.Invoke();
            }
        }
        
    }
    public void LerpObject(Transform lerpObject, Vector3 _finalScale, float speed, System.Action _lerpComplete = null)
    {
        currentObject = lerpObject;
        initScale = currentObject.transform.localScale;
        finalScale = _finalScale;
        lerpSpeed = speed;
        lerpTime = 0;
        if(_lerpComplete != null)
            lerpComplete = _lerpComplete;
        else
            lerpComplete = null;

        toLerp = true;
    }
}
