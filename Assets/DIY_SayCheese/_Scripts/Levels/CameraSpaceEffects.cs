using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpaceEffects : MonoBehaviour
{
    public static CameraSpaceEffects instance;
    public GameObject confetti;
    public GameObject twinkle;

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

    public void SpawnEFX(GameObject efx)
    {
        efx.SetActive(true);
        Timer.Delay(2f, () => { efx.SetActive(false); });
    }
    public void OnEfx(GameObject efx)
    {
        efx.SetActive(true);
    }
}
