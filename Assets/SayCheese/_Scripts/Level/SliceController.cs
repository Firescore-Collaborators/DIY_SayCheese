using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using BzKovSoft.ObjectSlicer.Samples;
using Deform;

public class SliceController : MonoBehaviour
{
    BendDeformer currentBendDeformer;

    [SerializeField] private Direction direction;

    GameObject currentCutObject;
    public GameObject levelObject;
    public GameObject knife;

    public CapsuleCollider knifeCollider;

    public BendDeformer knifeDeformer;

    Vector3 startPos;
    Vector3 moveDir;

    public float knifeDownLimit = 0.2f;
    public float knifeSpeed = 2f;
    public float levelObjectSpeed = 1f;

    bool mouseHeld;
    bool cutting;

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
        startPos = knife.transform.position;
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
        float posy = knife.transform.position.y;
        posy -= Time.deltaTime * knifeSpeed;
        if (posy <= knifeDownLimit)
        {
            posy = knifeDownLimit;
            KnifeReached();
        }
        knife.transform.position = new Vector3(knife.transform.position.x, posy, knife.transform.position.z);
    }

    void KnifeReached()
    {
        mouseHeld = false;
        LerpObjectPosition.instance.LerpObject(knife.transform, startPos, 0.2f);

        if (currentBendDeformer == null) return;

        currentBendDeformer.transform.parent = currentCutObject.transform;
        currentCutObject.GetComponent<Rigidbody>().isKinematic = false;

        cutting = false;
    }

    void OnSliced(GameObject cutObject)
    {
        cutting = true;
        BendDeformer bendDeformer = Instantiate(knifeDeformer, knifeDeformer.transform);
        bendDeformer.transform.localPosition = Vector3.zero;
        bendDeformer.transform.localRotation = Quaternion.Euler(Vector3.zero);
        bendDeformer.transform.localScale = Vector3.one;
        Deformable deform = cutObject.AddComponent<Deformable>();
        deform.AddDeformer(bendDeformer);
        currentCutObject = cutObject;
        currentBendDeformer = bendDeformer;
    }
}
