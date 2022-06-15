using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.3f;
    public float shakeAmount = 0.7f;

    [Button]
    public void Shake()
    {
        transform.DOShakePosition(shakeDuration, shakeAmount);
        print("Shake");
    }
}
