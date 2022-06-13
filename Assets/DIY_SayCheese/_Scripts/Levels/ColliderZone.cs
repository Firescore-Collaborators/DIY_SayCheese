using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderZone : MonoBehaviour
{
    public float count;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cheese")
        {
            count++;
            other.tag = "Untagged";
        }
    }
}
