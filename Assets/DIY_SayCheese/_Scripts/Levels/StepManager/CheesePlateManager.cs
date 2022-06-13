using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheesePlateManager : MonoBehaviour
{
    bool held = false;
    [SerializeField]bool drag;
    [SerializeField] LayerMask layer;
    public float interval = 0.2f;
    public float spawnHeight = 2f;
    float spawnTimer = 0;
    public GameObject spawnObject;
    public Transform spawnParent;
    Vector3 lastMousePos;

    void Update()
    {
        SetInput();
        SpawnFood();
    }

    void SetInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            held = true;
        }

        if(Input.GetMouseButtonUp(0))
        {
            held = false;
        }

        if(held)
        {
            if(Input.mousePosition != lastMousePos)
            {
                drag = true;
            }
            else
            {
                drag = false;
            }
        }
        else
        {
            drag = false;
        }

        lastMousePos = Input.mousePosition;
        
    }
    void SpawnFood()
    {
        if(!drag) return;

        spawnTimer += Time.deltaTime;

        if(spawnTimer < interval)
        {
            return;
        }

        spawnTimer = 0;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, 100, layer))
        {
            Instantiate(spawnObject, new Vector3(hit.point.x,hit.point.y + spawnHeight,hit.point.z), spawnObject.transform.rotation, spawnParent);
        }


    }

    

}
