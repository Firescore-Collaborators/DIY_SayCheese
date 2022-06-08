using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class ObjectRotateTween : MonoBehaviour
{
    public float speed1 = 1;
    public float speed2 = 2;
    public float lerpSpeed  = 1;
    public AnimationCurve curve;
    public float angle;

    public Transform target;
    public ObjectRotate objectRotate;

    Vector3 initPos;
    Quaternion initRot;

    void Start()
    {
        initPos = transform.position;
        initRot = transform.rotation;
    }

    public void LerpObject(System.Action callback)
    {
        Timer.Delay(1.0f, () =>
        {
            LerpObjectPosition.instance.LerpObject(transform, target.position, lerpSpeed, () =>
            {
                //Rotate(callback);
                
            });
            LerpObjectRotation.instance.LerpObject(transform, target.rotation, lerpSpeed, () =>
                {
                    //Rotate(callback);
                    //callback();
                    Timer.Delay(0.50f, () =>
                    {
                        //objectRotate.enabled = true;
                        callback();
                    });
                });

            /*LerpObjectRotation.instance.LerpObject(transform, target.rotation, lerpSpeed, () =>
            {
                LerpObjectRotation.instance.LerpObject(diskOpen, Quaternion.Euler(-45.0f, 0, 0), lerpSpeed, () =>
                {
                    callback();
                    objectRotate.enabled = true;
                    Timer.Delay(2.0f, () =>
                    {
                        LerpObjectRotation.instance.LerpObject(diskOpen, Quaternion.Euler(22.283f,0,0), lerpSpeed);
                        Timer.Delay(1.0f, () =>
                        {
                            confetti.SetActive(true);
                        });
                    });
                });
            });
            */


        });

    }

    public void ConfettiEnabled()
    {
        Timer.Delay(.5f, () =>
        {
        });
    }


    [Button]
    void LerpToDefault()
    {
        LerpObjectPosition.instance.LerpObject(transform, initPos, lerpSpeed);

        LerpObjectRotation.instance.LerpObject(transform, initRot, lerpSpeed);
    }

    public void Rotate(System.Action callback)
    {
        /*transform.DOLocalRotate(new Vector3(transform.localRotation.eulerAngles.x,angle,0),speed1,RotateMode.LocalAxisAdd).SetEase(Ease.Linear);

        transform.DOLocalRotate(new Vector3(transform.localRotation.eulerAngles.x,angle,0),speed,RotateMode.LocalAxisAdd).SetEase(curve).SetDelay(speed1).onComplete += ()=>
        {
            rotateEnd.Raise();
        };
        */
        transform.LeanRotateAroundLocal(Vector3.up, angle, speed1).setEase(LeanTweenType.linear);
        transform.LeanRotateAroundLocal(Vector3.up, angle, speed2).setEase(curve).setDelay(speed1).setOnComplete(() =>
        {
            //LerpToDefault();
            callback();
        });

    }
}
