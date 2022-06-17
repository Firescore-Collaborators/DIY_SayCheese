using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//capsule object to build expected cutting line
public class CapsuleScript : MonoBehaviour
{
    ParticleSystem.EmissionModule em;
    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        em = GetComponent<ParticleSystem>().emission;
        em.enabled = false;
    }

    //effect after cut
    private void OnTriggerEnter(Collider other)
    {
        em.enabled = true;
        GetComponent<ParticleSystem>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
        Debug.Log("Collision Detected");
    }
}
