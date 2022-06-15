using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheesePastaPlateManager : MonoBehaviour
{
    public ParticleSystem particles;
    bool held = false;
    [SerializeField] bool canPlay;
    bool CanPlay
    {
        get
        {
            return canPlay;
        }

        set
        {
            canPlay = value;
            objectFollowMouse.enabled = value;
        }
    }

    public ObjectFollowMouse objectFollowMouse;

    void Start()
    {
        CanPlay = false;
        Timer.Delay(2.0f, () =>
        {
            CanPlay = true;
        });
    }
    void Update()
    {
        SetInput();
        ParticleState();
    }

    void SetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            held = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            held = false;
        }
    }

    void ParticleState()
    {

        if (held)
        {
            if (!CanPlay) return;

            if (!particles.isPlaying)
            {
                particles.Play();
            }
        }
        else
        {
            if (particles.isPlaying)
            {
                particles.Stop();
            }
        }
    }
}
