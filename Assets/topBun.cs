using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class topBun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Timer.Delay(5, TopBunMove);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TopBunMove()
    {
        gameObject.transform.DOLocalMoveY(-0.44f, 4).SetEase(Ease.InExpo);
    }
}
