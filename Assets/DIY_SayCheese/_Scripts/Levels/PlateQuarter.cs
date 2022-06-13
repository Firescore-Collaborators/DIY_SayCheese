using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateQuarter : MonoBehaviour
{
    public float count;
    public float currentCount;
    void Start()
    {
        count = transform.childCount;
    }

    public void Increase()
    {
        currentCount++;
    }


}   
