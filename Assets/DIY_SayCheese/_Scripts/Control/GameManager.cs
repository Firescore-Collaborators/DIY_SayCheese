using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using NaughtyAttributes;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get{
            return GameObject.Find("GameManager").GetComponent<GameManager>();
        }        
    }
    public static event System.Action Reset;


    [Expandable]
    public LevelContainer levels;
    public LevelSO CurrentLevel
    {
        get
        {
            return levels.CurrentLevelSO;
        }
    }

    public int currentStepIndex = 0;

    public StepSO CurrentStep
    {
        get
        {
            return CurrentLevel.steps[currentStepIndex];
        }
    }

    public Transform lvlObjSpawnPoint;
    public LevelObject levelObject;
    public GameStepState currentStepState;
    public Joystick joystick;

    void SwitchStep(StepType stepType)
    {
        if(StepStateMachine.stepStates.ContainsKey(stepType))
        {
            Debug.Log("Current step = "+stepType);
            currentStepState = GetComponent(StepStateMachine.stepStates[stepType]) as GameStepState;
            if(currentStepState != null)
            {
                currentStepState.OnStepStart();
            }
            else{
                Debug.LogError(stepType+" step state not found");
            }
            
        }
    }
    private void OnEnable() {
        CurrentLevel.Reset += OnLevelReset;
        int levelCounter = PlayerPrefs.GetInt("LevelCount");
        levelCounter++;
        PlayerPrefs.SetInt("LevelCount", levelCounter);
    }

    private void OnDisable() {
        CurrentLevel.Reset -= OnLevelReset;
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        currentStepIndex = 0;
        LoadLevelObject();
        CurrentLevel.OnLevelStart();
        BeginStep();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            NextStep();
        }    
    }

    void LoadLevelObject()
    {
        if(CurrentLevel.levelObject != null)
        {
            GameObject lvlObj = Instantiate(CurrentLevel.levelObject, lvlObjSpawnPoint);
            lvlObj.transform.localPosition = Vector3.zero;  
            lvlObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
            lvlObj.transform.localScale = Vector3.one;
            levelObject = lvlObj.GetComponent<LevelObject>();
        }
    }
    void BeginStep()
    {
        if(currentStepIndex < CurrentLevel.steps.Count)
        {
            SwitchStep(CurrentStep.stepType);

            CurrentStep.OnStepStart();
        }

    }

    public void NextStep()
    {
        CurrentStep.OnStepEnd();

        if(currentStepState != null)
        {
            currentStepState.OnStepEnd();
        }
        if(currentStepIndex < CurrentLevel.steps.Count - 1)
        {
            currentStepIndex++;
            BeginStep();
        }
        else{
            
            SwitchStep(StepType.levelEnd);
            CurrentLevel.OnLevelEnd();
        }
    }

    void OnLevelReset()
    {
        Reset?.Invoke();
    }

}
