using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SplineMesh;
using Deform;
using TMPro;
using UnityEngine.SceneManagement;
//using 

public class HorizonStepManager : MonoBehaviour
{
    public GameObject cheesePrefab;
    public GameObject cheeseSlicer;
    public GameObject topFood;
    public GameObject bottomFood;
    public GameObject foodStartPos;
    public Joystick joystick;
    public GameObject lowerSandwich;
    public GameObject upperSandwich;
    public GameObject topMeshObj;
    public GameObject topMeshPos;
    public GameObject FoodObject;
    public GameObject Confetti;
    public GameObject testObj;
    public Canvas scoreCavas;
    public TextMeshProUGUI scoreText;
    public LevelScore levelScore;
    public GameObject finalFoodHolder;
    public Canvas nextLevelCanvas;

    public enum HorizontalCuttingStates
    {
        cutYetToBegin,
        cutting,
        cutComplete
     }

    public HorizontalCuttingStates cutState;

    private FoodScript topScript;
    private FoodScript bottomScript;
    private float canvasInitialX;
    private KnifeScript knifeScript;

    private static HorizonStepManager _instance;
    public static HorizonStepManager Inst
    {
        get
        {
            if (_instance == null)
                Debug.Log(typeof(HorizonStepManager) + " is missing");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = (HorizonStepManager)this;

    }

    float percent;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (HorizonStepManager.Inst.cutState == HorizonStepManager.HorizontalCuttingStates.cutYetToBegin)
            {
                HorizonStepManager.Inst.cutState = HorizonStepManager.HorizontalCuttingStates.cutting;
            }
        }
        if (HorizonStepManager.Inst.cutState == HorizonStepManager.HorizontalCuttingStates.cutting)
        {
            percent = Mathf.RoundToInt(LevelScore.percentageAccuracy);
            knifeScript.MoveKnife(joystick.Vertical);
            scoreCavas.transform.position =new Vector3( canvasInitialX + cheeseSlicer.transform.position.x, scoreCavas.transform.position.y, scoreCavas.transform.position.z);
            scoreText.GetComponent<RectTransform>().localScale =new Vector3( .075f +  percent* .025f/100, .075f + percent * .025f/100, scoreCavas.GetComponent<RectTransform>().localScale.z);
            scoreText.text = percent.ToString() + "%";
        }
    }

    private void Start()
    {
        cutState = HorizontalCuttingStates.cutYetToBegin;
        topScript = topFood.GetComponentInChildren<FoodScript>();
        bottomScript = bottomFood.GetComponentInChildren<FoodScript>();
        topScript.MeshInitialisation();
        bottomScript.MeshInitialisation();
        //cheeseParentInitialX = parentForCheeses.transform.position.x;
        knifeScript = cheeseSlicer.GetComponent<KnifeScript>();
        canvasInitialX = scoreCavas.transform.position.x - cheeseSlicer.transform.position.x;
    }

    public void CheckCutComplete()
    {
        Debug.Log("HorizonManager check cut complete called");
        //if (topScript.cutComplete && bottomScript.cutComplete)
        
        {
            cutState = HorizontalCuttingStates.cutComplete;
        }
    }
    public float GetCheeseParentMoveX()
    {
        float moved = 0;
        //moved =cheeseParentInitialX - parentForCheeses.transform.position.x;
        return moved;
    }

    public void AfterCut()
    {
        CameraController.instance.SetCurrentCamera("Sandwich", 0);
        //topFood.transform.localPosition = new Vector3(-.15f, -2f, -32.25f);
        
        Vector3 temp = topFood.GetComponent<Spline>().nodes[0].Position;
        topFood.GetComponent<Spline>().nodes[1].Position = new Vector3(temp.x, temp.y, temp.z + 10);
        topFood.GetComponent<Spline>().nodes[1].Direction = topFood.GetComponent<Spline>().nodes[1].Position;
        topMeshObj.GetComponent<MeshBender>().rate = 0;
        topMeshObj.GetComponent<MeshBender>().Contort();
        topMeshPos.GetComponent<MeshFilter>().mesh = topMeshObj.GetComponent<MeshFilter>().mesh;
        topMeshPos.transform.DOLocalMoveY(-0.4f, 2f).SetEase(Ease.InExpo).OnComplete(Afterfall);
        //topMeshPos.transform.DOLocalMoveY(-.75f, 1f).OnComplete(Afterfall);

        upperSandwich.transform.DOLocalMoveY(-0.91f, 3f).SetEase(Ease.InExpo);
    }

    public void Afterfall()
    {
        topMeshPos.transform.DOShakeScale(.5f,.3f,10,10,true);
        SineDeformer sine;
        sine = topMeshPos.GetComponentInChildren<SineDeformer>();
        DOTween.To(() => sine.Frequency, x => sine.Frequency = x, 0, .5f);
        DOTween.To(() => sine.Amplitude, x => sine.Amplitude = x, 0, .5f);
        DOTween.To(() => sine.Speed, x => sine.Speed = x, 0, .5f);
        
        Timer.Delay(2.5f, EndAnimations);
    }

    public void EndAnimations()
    {
        
        CameraController.instance.SetCurrentCamera("EndCam", 2);
        FoodObject.transform.DOLocalMoveY(15f, 2f).OnComplete(EnableConfetti);
    }

    public void EnableConfetti()
    {
        finalFoodHolder.transform.localPosition = upperSandwich.transform.localPosition;
        topMeshPos.gameObject.transform.SetParent(finalFoodHolder.transform);
        lowerSandwich.gameObject.transform.SetParent(finalFoodHolder.transform);
        upperSandwich.gameObject.transform.SetParent(finalFoodHolder.transform);
        Vector3 rotateVector = new Vector3(finalFoodHolder.transform.rotation.eulerAngles.x, -180, finalFoodHolder.transform.rotation.eulerAngles.z);
        finalFoodHolder.transform.DOLocalRotate(rotateVector, 2f, RotateMode.Fast).SetLoops(-1,LoopType.Incremental).SetEase(Ease.Linear);
        Confetti.SetActive(true);
        Timer.Delay(3.5f, LoadNextLevelCanvas);
    }
     public void LoadNextLevelCanvas()
    {
        nextLevelCanvas.gameObject.SetActive(true);
        Confetti.gameObject.SetActive(false);
    }
    public void NextLevelButton()
    {
        nextLevelCanvas.gameObject.SetActive(false);
        GameManager.Instance.levels.CurrentLevel++;
        if(GameObject.Find("/Timer")!=null)
        {
            Destroy(GameObject.Find("/Timer"));
        }
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}
