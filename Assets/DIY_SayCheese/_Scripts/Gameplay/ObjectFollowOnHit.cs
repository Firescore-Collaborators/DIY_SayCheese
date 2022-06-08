using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowOnHit : MonoBehaviour
{
    Camera cam;
    Ray ray;
    public Vector3 offset;
    public float valueY;
    public GameObject hitObject;
    public LayerMask layerMask;
    public bool shouldOffset = false;

    void OnEnable()
    {
        Start();
    }

    void Start()
    {
        cam = Camera.main;
        ray = cam.ScreenPointToRay(Input.mousePosition);
        valueY = transform.position.y;

        if(!shouldOffset) return;
        
        if (Physics.Raycast(ray, out RaycastHit hit, layerMask))
        {
            offset = hit.point - transform.position;
        }
    }

    void Update()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100, layerMask))
        {
            hitObject = hit.collider.gameObject;
            Vector3 newPoint = hit.point - offset;
            transform.position = new Vector3(newPoint.x, valueY, newPoint.z);
        }

    }
}
