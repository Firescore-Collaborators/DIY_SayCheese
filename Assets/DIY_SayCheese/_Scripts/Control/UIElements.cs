using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElements : MonoBehaviour
{
    public static UIElements instance;
    public Transform stepPanel;
    public GameObject stepUI;

    public List<GameObject> stepUIs = new List<GameObject>();
    public Image blinkPanel;

    public void OnEnable()
    {
        if(GameManager.Instance != null)
        GameManager.Instance.CurrentLevel.Start += SpawnStepUI;
    }

    // public void OnDisable()
    // {
    //     GameManager.Instance.CurrentLevel.Start -= SpawnStepUI;
    // }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SpawnStepUI()
    {
        for(int i = 0; i < GameManager.Instance.CurrentLevel.steps.Count; i++)
        {
            GameObject stepUIObj = Instantiate(stepUI, stepPanel);
            stepUIs.Add(stepUIObj);
        }
    }

    public void StepStart()
    {
        stepUIs[GameManager.Instance.currentStepIndex].transform.GetChild(0).gameObject.SetActive(true);
    }
    public void StepComplete()
    {
        RectTransform currentTransform = ((RectTransform)stepUIs[GameManager.Instance.currentStepIndex].transform.GetChild(0));
        currentTransform.localPosition = Vector3.zero;
        currentTransform.sizeDelta = Vector2.zero;
    }

    public void LevelComplete()
    {
        stepPanel.gameObject.SetActive(false);
    }

}
