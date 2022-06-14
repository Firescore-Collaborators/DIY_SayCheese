using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseGratingManager : MonoBehaviour
{
    public MeshRenderer cheeseBlock;

    public float cheeseMaskLerpSpeed = 12f;
    public float cheeseMaskLerpValue = 0;
    public bool held = false;
    public bool toMove = true;
    bool isMoving = false;
    bool coroutineStarted = false;
    public float cheeseMoveSpeed = 0.11f;
    public float dragSpeed = 0.1f;
    public Direction direction = Direction.Down;
    Vector3 moveDir;
    Vector3 lastMousePos;
    public float dragDir;
    public ParticleSystem cheeseParticle;

    void Start()
    {
        AssignDirection();
    }

    void Update()
    {
        SetInput();
        IsMoving();
        CheeseMove();
        CheeseParticle();
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
            Vector3 dragMouse = Input.mousePosition - lastMousePos;
            dragDir = Mathf.Clamp(dragMouse.x, -15f, 15f);
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
        if (!isMoving) return;

        cheeseBlock.transform.position += moveDir * cheeseMoveSpeed * Time.deltaTime;

        //Cheese Mask
        CheeseMask();

        //Horizontal
        cheeseBlock.transform.position += cheeseBlock.transform.right * dragDir * dragSpeed * Time.deltaTime;

        // Vector3 horizontalPos = (cheeseBlock.transform.position) + (cheeseBlock.transform.right * dragDir * dragSpeed * Time.deltaTime);
        // float posX = Mathf.Clamp(horizontalPos.x, -2.311f, -1.62f);
        // if (posX == -2.311f && dragDir > 0 || posX == -1.62f && dragDir < 0) return;

        //cheeseBlock.transform.position = new Vector3(posX, horizontalPos.y, horizontalPos.z);
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
    }

    void IsMoving()
    {
        if (dragDir == 0)
        {
            if (coroutineStarted) return;

            coroutineStarted = true;
            StartCoroutine(ChangeIsMove());
        }
        else
        {
            StopAllCoroutines();
            coroutineStarted = false;
            isMoving = true;
        }
    }

    IEnumerator ChangeIsMove()
    {
        yield return new WaitForSeconds(1);
        isMoving = false;
    }
}
