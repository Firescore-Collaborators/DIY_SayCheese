using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatGun : MonoBehaviour
{

    public bool isHeating;
    Collider gunCollider;
    public Transform rotationLeft;
    public Transform rotationRight;
    public ParticleSystem fire;
    void Start()
    {
        gunCollider = GetComponent<Collider>();
    }
    void Update()
    {
        UpdateRotation();

        if (Input.GetMouseButtonDown(0))
        {
            gunCollider.enabled = true;
            if(!fire.isPlaying)
            {
                fire.Play();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            gunCollider.enabled = false;
            isHeating = false;
            if (fire.isPlaying)
            {
                fire.Stop();
            }
        }
    }

    void UpdateRotation()
    {
        transform.parent.transform.rotation = Quaternion.Lerp(rotationLeft.rotation, rotationRight.rotation, Remap.remap(transform.parent.transform.position.x, rotationLeft.position.x, rotationRight.position.x, 0, 1));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cheese")
        {
            isHeating = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cheese")
        {
            isHeating = false;
        }
    }


}
