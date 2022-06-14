using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseGratingManager : MonoBehaviour
{
    public MeshRenderer cheeseBlock;

    [Range(0, 1)]
    public float cheeseMoveValue = 0;
    bool toLerp = true;
    Vector3 cheeseStartPos;
    public Transform cheeseEndPos;

    void Start()
    {
        cheeseStartPos = cheeseBlock.transform.position;
    }

    void Update()
    {
        CheeseMove();
    }

    void CheeseMove()
    {
        if(!toLerp) return;

        cheeseBlock.transform.position = Vector3.Lerp(cheeseStartPos, cheeseEndPos.transform.position, cheeseMoveValue);

        //Cheese Mask
        float cheeseMaskValue = Remap.remap(cheeseMoveValue, 0, 1, 1, 0.1f);
        cheeseBlock.material.SetFloat("_DissolveAmount", cheeseMaskValue);
    }
}
