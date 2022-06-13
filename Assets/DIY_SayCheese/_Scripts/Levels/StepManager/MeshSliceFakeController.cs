using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deform;
using Deform.Masking;

public class MeshSliceFakeController : MonoBehaviour
{
    public System.Action Slice;
    public System.Action SliceFinished;
    public Direction direction;
    public GameObject currentBendDeform;
    public GameObject currentBoxMask;
    public GameObject boxMaskEndTransform;
    public Transform cutter;
    public Transform bend;
    public Transform levelObject;
    public Transform cutPos;
    public Transform cutterCutTransform;
    public Transform bendEndTransform;
    List<Transform> cheeseSlice = new List<Transform>();
    Vector3 moveDir;
    Quaternion cutterStartRot;
    [SerializeField] int stopSliceAt = 3;
    [SerializeField] bool canMove = true;
    [SerializeField] bool toCheck;
    bool toLerp;
    float lerpTime;
    public float lerpSpeed;
    [SerializeField] float distance;
    [SerializeField] float forceAtEnd = 3f;
    [SerializeField] float cutSpeed = 3f;
    [SerializeField] float maskMoveSpeed = 3f;
    public float levelObjectSpeed = 1f;
    int currentSliceIndex = 0;

    LevelObject levelObjectScript
    {
        get
        {
            return GameManager.Instance.levelObject;
        }
    }
    void OnEnable()
    {
        Slice += OnSlice;
        SliceFinished += OnSliceFinished;
    }

    void OnDisable()
    {
        Slice -= OnSlice;
        SliceFinished -= OnSliceFinished;
    }

    void Start()
    {
        levelObject = GameManager.Instance.levelObject.transform;
        InitSlices();
        AssingDir();
        cutterStartRot = cutter.transform.rotation;
    }

    void InitSlices()
    {
        for (int i = 0; i < levelObject.transform.childCount; i++)
        {
            cheeseSlice.Add(levelObject.transform.GetChild(i));
        }
    }

    void Update()
    {
        Movement();
        CheckForSlice();
        LerpBoxMask();
    }


    void Movement()
    {
        if (!canMove) return;

        levelObject.transform.position += moveDir * Time.deltaTime * levelObjectSpeed;
    }

    void CheckForSlice()
    {
        if (!toCheck) return;
        distance = Vector3.Distance(cutPos.transform.position, cheeseSlice[currentSliceIndex].position);
        if (distance < 0.01f)
        {
            canMove = false;
            toCheck = false;
            AssignBendDeformer();
            Slice?.Invoke();
        }
    }

    void OnSlice()
    {
        LerpObjectRotation.instance.LerpObject(cutter, cutterCutTransform.rotation, cutSpeed);
        currentBoxMask.AddComponent<LerpObjectPositionBehavior>().LerpObject(currentBoxMask.transform, boxMaskEndTransform.transform.position, maskMoveSpeed);
        Timer.Delay(1.75f,()=>
        {
            toLerp = true;
        });
        LerpObjectPosition.instance.LerpObject(currentBendDeform.transform, bendEndTransform.position, cutSpeed, () =>
        {
            SliceFinished?.Invoke();
        });
    }

    void LerpBoxMask()
    {
        if(!toLerp) return;
        
        currentBoxMask.GetComponent<BoxMask>().Factor = Mathf.Lerp(1, 0, lerpTime);
        if(lerpTime < 1.0f)
        {
            lerpTime += Time.deltaTime/lerpSpeed;
        }
        else{
            toLerp = false;
            lerpTime = 0;
        }
        
    }

    void OnSliceFinished()
    {
        currentBendDeform.transform.parent = null;
        Rigidbody rb = cheeseSlice[currentSliceIndex].gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(moveDir * forceAtEnd, ForceMode.Impulse);
        cheeseSlice[currentSliceIndex].transform.parent = null;
        LerpObjectRotation.instance.LerpObject(cutter, cutterStartRot, 0.5f, () =>
        {
            currentSliceIndex++;
            canMove = true;
            toCheck = true;
            if (currentSliceIndex >= stopSliceAt)
            {
                this.enabled = false;
            }
        });
    }


    void AssignBendDeformer()
    {
        BendDeformer bendDeformer = Instantiate(bend, bend).GetComponent<BendDeformer>();
        Deformable deform = cheeseSlice[currentSliceIndex].gameObject.AddComponent<Deformable>();
        bendDeformer.transform.localPosition = Vector3.zero;
        bendDeformer.transform.localRotation = Quaternion.Euler(Vector3.zero);
        bendDeformer.transform.localScale = Vector3.one;
        deform.AddDeformer(bendDeformer);
        currentBoxMask = bendDeformer.transform.GetChild(0).gameObject;
        deform.AddDeformer(currentBoxMask.GetComponent<Deformer>());
        currentBendDeform = bendDeformer.gameObject;
    }

    void AssingDir()
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

}
