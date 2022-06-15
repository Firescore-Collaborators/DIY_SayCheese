using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NaughtyAttributes;

public class CheesePlateManager : MonoBehaviour
{
    /*public List<ColliderZone> colliderZone = new List<ColliderZone>();
    bool held = false;
    [SerializeField] bool drag;
    [SerializeField] LayerMask layer;
    public float zoneCount = 10f;
    public float interval = 0.2f;
    public float spawnHeight = 2f;
    float spawnTimer = 0f;
    public Image fillImage;
    public GameObject spawnObject;
    public Transform spawnParent;
    public Transform cheesePlate;
    Vector3 lastMousePos;
    int currentZone = 0;

    void Update()
    {
        SetInput();
        SpawnFood();
        Progress();
    }

    void SetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            held = true;
            Spawn();
        }

        if (Input.GetMouseButtonUp(0))
        {
            held = false;
        }

        if (held)
        {
            if (Input.mousePosition != lastMousePos)
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
        if (!drag) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer < interval)
        {
            return;
        }

        spawnTimer = 0;

        Spawn();
    }

    void Spawn()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100, layer))
        {
            GameObject cheese = Instantiate(spawnObject, new Vector3(hit.point.x, hit.point.y + spawnHeight, hit.point.z), spawnObject.transform.rotation, spawnParent);
            Timer.Delay(1.5f, () =>
            {
                cheese.GetComponent<Rigidbody>().isKinematic = true;
            });
        }
    }

    void Progress()
    {
        float currentValue = colliderZone[currentZone].count;
        fillImage.fillAmount = Remap.remap(currentValue, 0, zoneCount, (currentZone * (1f / colliderZone.Count)), ((currentZone + 1) * (1f / colliderZone.Count)));
        if (currentValue >= zoneCount)
        {
            currentZone++;
            if (currentZone >= colliderZone.Count)
            {
                this.enabled = false;
                Timer.Delay(2f, () =>
                {
                    GameManager.Instance.NextStep();
                });
                return;
            }
            Rotate();
        }
    }


    [Button]
    void Rotate()
    {
        LerpObjectRotation.instance.LerpObject(cheesePlate, Quaternion.Euler(cheesePlate.rotation.eulerAngles.x,
                                                                            cheesePlate.rotation.eulerAngles.y + 90.0f,
                                                                            cheesePlate.rotation.eulerAngles.z), 0.5f);
    }
    */

}
