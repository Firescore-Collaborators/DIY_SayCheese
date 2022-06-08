using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;
public class RespondMessage : MonoBehaviour
{
    public List<string> correctMessage = new List<string>();
    public List<string> wrongMessage = new List<string>();
    public float scaleValue = 1.2f;
    public float moveValue = 20f;
    public Text messageText;
    public Vector3 startPos;
    DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> scaleTween;
    DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> moveTween;
    void Start()
    {
        startPos = messageText.transform.position;
    }
    public void ShowCorrectResponse()
    {
        int rand = Random.Range(0, correctMessage.Count);
        messageText.text = correctMessage[rand];
        MoveText();
    }

    [Button]
    void MoveText()
    {
        scaleTween.Kill();
        moveTween.Kill();
        messageText.transform.position = startPos;
        messageText.gameObject.SetActive(true);
        scaleTween = messageText.transform.DOScale(messageText.transform.localScale + (Vector3.one * scaleValue), 0.2f).SetLoops(2, LoopType.Yoyo);
        moveTween = messageText.transform.DOMoveY(messageText.transform.position.y + moveValue, 2f).OnComplete(() =>
        {
            messageText.gameObject.SetActive(false);
        });
    }

}
