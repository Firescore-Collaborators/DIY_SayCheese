using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using BzKovSoft.ObjectSlicer.Samples;
using Deform;

public class MeshSliceController : MonoBehaviour
{
    BendDeformer currentBendDeformer;

    [SerializeField] private Direction direction;

    GameObject currentCutObject;
    public GameObject levelObject;
    public GameObject knife;

    public CapsuleCollider knifeCollider;

    public BendDeformer knifeDeformer;

    public Transform knifeCutTransform;
    Quaternion startRot;
    Vector3 moveDir;

    public float knifeSpeed = 2f;
    public float levelObjectSpeed = 1f;
    float knifeLerp;
    bool mouseHeld;
    bool cutting;
    bool canCut = true;
    void OnEnable()
    {
        Sliceable.sliced += OnSliced;
    }

    void OnDisable()
    {
        Sliceable.sliced -= OnSliced;
    }

    void Start()
    {
        startRot = knife.transform.rotation;
        canCut = true;
        AssingMoveDir();
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseButtonDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnMouseButtonUp();
        }

        KnifeMovement();
        LevelObjectMovement();
    }


    void OnMouseButtonDown()
    {
        knifeCollider.enabled = true;
        mouseHeld = true;
    }

    void OnMouseButtonUp()
    {
        knifeCollider.enabled = false;
        mouseHeld = false;
    }

    void AssingMoveDir()
    {
        switch (direction)
        {
            case Direction.Up:
                moveDir = levelObject.transform.up;
                break;
            case Direction.Down:
                moveDir = -levelObject.transform.up;
                break;
            case Direction.Left:
                moveDir = -levelObject.transform.right;
                break;
            case Direction.Right:
                moveDir = levelObject.transform.right;
                break;
            case Direction.Forward:
                moveDir = levelObject.transform.forward;
                break;
            case Direction.Back:
                moveDir = -levelObject.transform.forward;
                break;
        }
    }
    void LevelObjectMovement()
    {
        if (mouseHeld) return;
        if (cutting) return;
        levelObject.transform.position += moveDir * Time.deltaTime * levelObjectSpeed;
    }

    void KnifeMovement()
    {
        if (!mouseHeld) return;
        if (!canCut) return;

        knifeLerp += Mathf.Clamp(Time.deltaTime * knifeSpeed, 0, 1);
        if (knifeLerp >= 1)
        {
            knife.transform.rotation = Quaternion.Lerp(startRot, knifeCutTransform.rotation, 1);
            KnifeReached();
        }
       
        knife.transform.rotation = Quaternion.Lerp(startRot, knifeCutTransform.rotation, knifeLerp);
    }

    void KnifeReached()
    {

        mouseHeld = false;
        canCut = false;
        LerpObjectRotation.instance.LerpObject(knife.transform, startRot, 0.2f, () =>
        {
            canCut = true;
        });
        knifeLerp = 0;

        if (currentBendDeformer == null) return;

        currentBendDeformer.transform.parent = currentCutObject.transform;
        currentCutObject.GetComponent<Rigidbody>().isKinematic = false;

        cutting = false;

    }

    void OnSliced(GameObject cutObject)
    {
        cutting = true;
        /*BendDeformer bendDeformer = Instantiate(knifeDeformer, knifeDeformer.transform);
        bendDeformer.transform.localPosition = Vector3.zero;
        bendDeformer.transform.localRotation = Quaternion.Euler(Vector3.zero);
        bendDeformer.transform.localScale = Vector3.one;
        Deformable deform = cutObject.AddComponent<Deformable>();
        deform.AddDeformer(bendDeformer);
        currentCutObject = cutObject;
        currentBendDeformer = bendDeformer;
        */
    }
}
