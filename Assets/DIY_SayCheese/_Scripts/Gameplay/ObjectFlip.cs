using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class ObjectFlip : MonoBehaviour
{
    public Transform target;
    public float lerpTime = 2f;

    Vector3 initPos;
    Quaternion initRot;

    Vector3 targetPos;
    void Start()
    {
        initPos = transform.position;
        initRot = transform.rotation;
        targetPos = target.position;
    }

    [Button]
    public void Flip()
    {
        LerpObjectPosition.instance.LerpObject(transform, targetPos, lerpTime, () =>
        {
            LerpObjectRotation.instance.LerpObject(transform, target.rotation, lerpTime, () =>
            {
                LerpObjectPosition.instance.LerpObject(transform, initPos, lerpTime);
            });
        });
    }

    
}
