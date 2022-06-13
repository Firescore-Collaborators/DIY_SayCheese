using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NaughtyAttributes;

public class CheeseOrganizeManager : MonoBehaviour
{

    public List<PlateQuarter> plateQuarters = new List<PlateQuarter>();
    bool held = false;
    [SerializeField] bool drag;
    public GameObject spawnObject;
    public Transform cheesePlate;
    public Transform spawnParent;
    Vector3 lastMousePos;
    public Image fillImage;
    [SerializeField] LayerMask layer;
    public float spawnHeight = 2.5f;
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
        if (!held) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100, layer))
        {
            if (hit.collider.tag == "Cheese")
            {
                GameObject cheese = Instantiate(spawnObject, new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + spawnHeight, hit.collider.transform.position.z),
                                                 hit.collider.transform.rotation, spawnParent);

                cheese.AddComponent<LerpObjectPositionBehavior>().LerpObject(cheese.transform, hit.collider.transform.position, 0.5f, () =>
                {
                    /*PlateQuarter plateQuarter = plateQuarters[currentZone];
                    print(plateQuarter.currentCount);
                    if (plateQuarter.currentCount == plateQuarter.count)
                    {
                        Rotate();
                        currentZone++;
                    }*/
                });
                hit.collider.enabled = false;
                hit.collider.transform.parent.GetComponent<PlateQuarter>().Increase();
            }
        }
    }

    void Progress()
    {
        float currentValue = plateQuarters[currentZone].currentCount;
        fillImage.fillAmount = Remap.remap(currentValue, 0, plateQuarters[currentZone].count, (currentZone * (1f / plateQuarters.Count)), ((currentZone + 1) * (1f / plateQuarters.Count)));
        if (currentValue >= plateQuarters[currentZone].count)
        {
            currentZone++;
            if (currentZone >= plateQuarters.Count)
            {
                this.enabled = false;
                Timer.Delay(2f, () =>
                {
                    GameManager.Instance.NextStep();
                });
                return;
            }
            Timer.Delay(1.5f, () =>
            {
                Rotate();
            });
        }
    }

    [Button]
    void Rotate()
    {
        LerpObjectRotation.instance.LerpObject(cheesePlate, Quaternion.Euler(cheesePlate.rotation.eulerAngles.x,
                                                                            cheesePlate.rotation.eulerAngles.y + 90.0f,
                                                                            cheesePlate.rotation.eulerAngles.z), 0.5f);
    }


}
