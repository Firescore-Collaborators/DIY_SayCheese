using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseGratingManager : MonoBehaviour
{

    public static event System.Action cheeseBlobSpawned;
    public CameraShake cameraShake;
    public MeshRenderer cheeseBlock;
    public GameObject emoji;
    [SerializeField] List<SkinnedMeshRenderer> cheeseList = new List<SkinnedMeshRenderer>();
    List<Animator> currentPlayingAnimators = new List<Animator>();
    public float cheeseFinishAmount = 30.0f;
    public float currentCheeseAmount;
    public float cheeseMaskLerpSpeed = 12f;
    public float cheeseMaskLerpValue = 0;
    bool toSetInput = true;
    public bool held = false;
    public bool toMove = true;
    public bool toSpawnBlob = true;
    public bool isMoving = false;
    bool coroutineStarted = false;
    public float cheeseMoveSpeed = 0.11f;
    public float dragSpeed = 0.1f;
    public float dragLimit = 20f;
    public Direction direction = Direction.Down;
    Vector3 moveDir;
    Vector3 lastMousePos;
    Vector3 lastCheesPos;
    Vector3 cheeseStartPos;
    public Vector2 horizontalLimit = new Vector2(-2.46f, -1.62f);
    public float dragDir;
    public ParticleSystem cheeseParticle;
    public Transform cheeseEndPos;
    Transform cheeseParent;
    public float gratedAmount;
    public float shakeDuration;
    public float cheeseSpawnAmount = 0.3f;
    public float cameraShakeInterval = 0.5f;

    public float emojiSpawnInterval = 2f;
    float currentEmojiTimer;
    void Start()
    {
        Init();
    }

    void Init()
    {
        cheeseStartPos = cheeseBlock.transform.position;
        cheeseParent = cheeseBlock.transform.parent;
        lastCheesPos = cheeseParent.transform.position;
        //AddToList(cheeseBlobParent,cheeseBlobs);
        AssignDirection();
    }

    void Update()
    {
        SetInput();
        IsMoving();
        CheeseMove();
        //CheeseParticle();
        ControlCheeseAnim();
    }

    void SetInput()
    {
        if(!toSetInput) return;

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
            Vector3 dragMouse = Input.mousePosition - lastMousePos;
            dragDir = Mathf.Clamp(dragMouse.x, -dragLimit, dragLimit);
        }
        else
        {
            dragDir = 0;
        }
        lastMousePos = Input.mousePosition;
    }

    void AssignDirection()
    {
        switch (direction)
        {
            case Direction.Up:
                moveDir = cheeseBlock.transform.up;
                break;
            case Direction.Down:
                moveDir = -cheeseBlock.transform.up;
                break;
            case Direction.Left:
                moveDir = -cheeseBlock.transform.right;
                break;
            case Direction.Right:
                moveDir = cheeseBlock.transform.right;
                break;
            case Direction.Forward:
                moveDir = cheeseBlock.transform.forward;
                break;
            case Direction.Back:
                moveDir = -cheeseBlock.transform.forward;
                break;
        }
    }
    void CheeseMove()
    {
        if (!held) return;
        if (!toMove) return;

        //Horizontal
        Vector3 horizontalPos = (cheeseParent.transform.position) + (cheeseParent.transform.right * dragDir * dragSpeed * Time.deltaTime);
        float posX = Mathf.Clamp(horizontalPos.x, horizontalLimit.x, horizontalLimit.y);
        if (posX == horizontalLimit.x && dragDir > 0 || posX == horizontalLimit.y && dragDir < 0) return;

        cheeseParent.transform.position = new Vector3(posX, horizontalPos.y, horizontalPos.z);

        if (!isMoving)
        {
            if (cheeseParticle.isPlaying)
            {
                cheeseParticle.Stop();
            }
            return;
        }   

        cheeseBlock.transform.position += moveDir * cheeseMoveSpeed * Time.deltaTime;

        if (!cheeseParticle.isPlaying)
        {
            cheeseParticle.Play();
        }

        //Cheese Mask
        CheeseMask();

        //Cheese Amount
        currentCheeseAmount += Time.deltaTime;
        if (currentCheeseAmount >= cheeseFinishAmount)
        {
            toSetInput = false;
            cheeseParticle.Stop();
            this.enabled = false;
            Timer.Delay(1.0f, () =>
            {
                GameManager.Instance.NextStep();
            });
        }
        //Grated amount
        currentEmojiTimer += Time.deltaTime;
        gratedAmount += Time.deltaTime;
        shakeDuration += Time.deltaTime;

        if(currentEmojiTimer >= emojiSpawnInterval)
        {
            currentEmojiTimer = 0;
            emoji.SetActive(true);
            Timer.Delay(2.0f, () =>
            {
                emoji.SetActive(false);
            });
        }
        if(shakeDuration >= cameraShakeInterval)
        {
            cameraShake.Shake();
            shakeDuration = 0;
        }

        if (gratedAmount > cheeseSpawnAmount)
        {
            gratedAmount = 0;
            LerpRandomCheeese();
            LerpRandomCheeese();
            LerpRandomCheeese();
            LerpRandomCheeese();
            if (toSpawnBlob)
                cheeseBlobSpawned?.Invoke();
        }

        //cheeseBlock.transform.position += cheeseBlock.transform.right * dragDir * dragSpeed * Time.deltaTime;


    }

    void ControlCheeseAnim()
    {
        if (isMoving) return;

        for (int i = 0; i < currentPlayingAnimators.Count; i++)
        {
            currentPlayingAnimators[i].enabled = false;
        }
    }
    void CheeseParticle()
    {
        if (toMove && isMoving)
        {
            if (!cheeseParticle.isPlaying)
            {
                cheeseParticle.Play();
            }
        }
        else
        {
            if (cheeseParticle.isPlaying)
            {
                cheeseParticle.Stop();
            }
        }
    }
    void CheeseMask()
    {
        float cheeseMask = Remap.remap(cheeseMaskLerpValue, 0, 1, 1, .1f);
        cheeseBlock.material.SetFloat("_DissolveAmount", cheeseMask);

        if (cheeseMaskLerpValue < 1.0f)
        {
            cheeseMaskLerpValue += Time.deltaTime / cheeseMaskLerpSpeed;
        }
        else
        {
            cheeseMaskLerpValue = 1;
            toMove = false;
        }

        /*float cheeseMask = Remap.remap(cheeseBlock.transform.position.y,cheeseStartPos.y, cheeseEndPos.position.y, 1, .1f);
        cheeseBlock.material.SetFloat("_DissolveAmount", cheeseMask);
        */
    }

    void IsMoving()
    {
        if (!held)
        {
            if (cheeseParticle.isPlaying)
            {
                cheeseParticle.Stop();
            }
            return;
        }
        if (cheeseParent.transform.position.x < lastCheesPos.x)
        {
            StopAllCoroutines();
            isMoving = true;
        }
        else
        {
            StartCoroutine(ChangeIsMove());
        }
        lastCheesPos = cheeseParent.transform.position;
    }

    IEnumerator ChangeIsMove()
    {
        yield return new WaitForSeconds(.2f);
        isMoving = false;
    }

    void LerpRandomCheeese()
    {
        int random = Random.Range(0, cheeseList.Count);
        Animator anim = cheeseList[random].GetComponent<Animator>();
        anim.enabled = true;
        currentPlayingAnimators.Add(anim);
        anim.Play("Grate");
    }



    void AddToList<T>(Transform parent, List<T> list)
    {
        list.Clear();
        for (int i = 0; i < parent.childCount; i++)
        {
            list.Add(parent.GetChild(i).GetComponent<T>());
        }
    }
    void AddToList(Transform parent, List<GameObject> list)
    {
        list.Clear();
        for (int i = 0; i < parent.childCount; i++)
        {
            list.Add(parent.GetChild(i).gameObject);
        }
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireCube(center, size);
    // }
    // void SpawnRandomCheese()
    // {
    //     Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));
    //     GameObject cheese = Instantiate(cheeseOnGrate, pos, Quaternion.identity);
    //     cheese.transform.parent = cheeseParent;
    // }
}

