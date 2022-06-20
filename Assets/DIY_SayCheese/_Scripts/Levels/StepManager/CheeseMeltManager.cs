using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deform;

public class CheeseMeltManager : MonoBehaviour
{
    public List<PerlinNoiseDeformer> cheeseMeltDeformers = new List<PerlinNoiseDeformer>();
    public ParticleSystem cheeseBubbles;
    public HeatGun heatGun;
    [Range(0, 1f)]
    public float meltAmount;
    public float meltSpeed = 1f;
    public float maxAmount = 0.025f;
    public float maxParticles = 5;
    bool held = false;
    void Update()
    {
        SetInput();
        UpdateCheeseAmount();
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
    void UpdateCheeseAmount()
    {
        if (!held) return;

        if (!heatGun.isHeating) return;

        meltAmount += meltSpeed * Time.deltaTime;
        meltAmount = Mathf.Clamp(meltAmount, 0, 1);
        if(meltAmount >= 1)
        {
            StepComplete();
        }
        CheeseMeltAmount();
    }
    void CheeseMeltAmount()
    {
        foreach (PerlinNoiseDeformer deformer in cheeseMeltDeformers)
        {
            deformer.MagnitudeScalar = Remap.remap(meltAmount, 0, 1, 0, maxAmount);
        }
        ParticleSystem.EmissionModule cheesePfx = cheeseBubbles.emission;
        cheesePfx.rateOverTime = Remap.remap(meltAmount, 0, 1, 0, maxParticles);
    }

    void StepComplete()
    {
        heatGun.transform.parent.gameObject.SetActive(false);
    }

}
