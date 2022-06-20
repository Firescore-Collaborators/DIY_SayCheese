using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deform;

public class CheeseMeltManager : MonoBehaviour
{
    public List<PerlinNoiseDeformer> cheeseMeltDeformers = new List<PerlinNoiseDeformer>();
    public ParticleSystem cheeseBubbles;
    public HeatGun heatGun;
    [Range(0,1f)]
    public float meltAmount;
    public float maxAmount = 0.025f;
    public float maxParticles = 5;
    bool toChangeValue = true;

    void Update()
    {
        CheeseMeltAmount();
    }

    void CheeseMeltAmount()
    {
        if(!toChangeValue) return;

        foreach(PerlinNoiseDeformer deformer in cheeseMeltDeformers)
        {
            deformer.MagnitudeScalar = Remap.remap(meltAmount, 0, 1, 0, maxAmount);
        }
        ParticleSystem.EmissionModule cheesePfx = cheeseBubbles.emission;
        cheesePfx.rateOverTime = Remap.remap(meltAmount, 0, 1, 0, maxParticles);
    }

}
