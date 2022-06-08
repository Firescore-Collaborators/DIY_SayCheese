using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate1 : ObjectRotate
{
    public float speed;
    void Update()
    {
        transform.Rotate(Vector3.right * speed *Time.deltaTime,Space.Self);
    }
}
