using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterCollider : MonoBehaviour
{
    public MeshSliceFakeController sliceController;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CutterCheck")
        {
            sliceController.toLerp = true;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
