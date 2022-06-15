using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderZone : MonoBehaviour
{
    public List<SkinnedMeshRenderer> cheeseList = new List<SkinnedMeshRenderer>();
    public float gratedAmount;

    SkinnedMeshRenderer currentMesh;
    bool isGrating = true;

    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            cheeseList.Add(transform.GetChild(i).GetComponent<SkinnedMeshRenderer>());
        }
    }
    
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        isGrating = true;
    }

    void OnTriggerExit(Collider other)
    {
        isGrating = false;
    }
}
