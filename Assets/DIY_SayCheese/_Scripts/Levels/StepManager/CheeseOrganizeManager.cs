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
    public Transform lerpPos;
    public Transform spawnParent;
    public Transform panel;
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
                StepComplete();
                return;
            }
            Timer.Delay(1.5f, () =>
            {
                Rotate();
            });
        }
    }

    public void StepComplete()
    {
        CameraController.instance.SetCurrentCamera("PlatingFinish", 1);
        panel.gameObject.SetActive(false);
        Timer.Delay(1.5f, () =>
        {
            LerpObjectPosition.instance.LerpObject(cheesePlate, lerpPos.transform.position, 1f, () =>
            {
                LerpObjectLocalRotation.instance.LerpObject(cheesePlate, lerpPos.transform.rotation, 1f, () =>
                {
                    cheesePlate.GetComponent<ObjectRotate>().enabled = true;
                    Timer.Delay(2.0f, () =>
                    {
                        CameraSpaceEffects.instance.SpawnEFX(CameraSpaceEffects.instance.confetti);
                        Timer.Delay(0.5f,()=>
                        {
                            UIElements.instance.gameObject.GetComponent<RespondMessage>().ShowCorrectResponse(UIElements.instance.gameObject.GetComponent<RespondMessage>().correctList1);
                        });
                        Timer.Delay(2.0f, () =>
                        {
                            GameManager.Instance.NextStep();
                        });

                    });
                });
            });
        });
    }

    [Button]
    void Rotate()
    {
        LerpObjectRotation.instance.LerpObject(cheesePlate, Quaternion.Euler(cheesePlate.rotation.eulerAngles.x,
                                                                            cheesePlate.rotation.eulerAngles.y + 90.0f,
                                                                            cheesePlate.rotation.eulerAngles.z), 0.5f);
    }


}
