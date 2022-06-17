using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    public float speed;
    public Direction direction;
    Vector3 rotateDir;
    void AssingDirection()
    {
        switch(direction)
        {
            case Direction.Up:
                rotateDir = Vector3.up;
                break;
            case Direction.Down:
                rotateDir = Vector3.up;
                break;
            case Direction.Left:
                rotateDir = Vector3.right;
                break;
            case Direction.Right:
                rotateDir = Vector3.right;
                break;
            case Direction.Forward:
                rotateDir = Vector3.forward;
                break;
            case Direction.Back:    
                rotateDir = Vector3.forward;
                break;
        }
    }

    void Start()
    {
        AssingDirection();
    }
    void Update()
    {
        transform.Rotate(rotateDir * speed *Time.deltaTime,Space.Self);
    }
}
