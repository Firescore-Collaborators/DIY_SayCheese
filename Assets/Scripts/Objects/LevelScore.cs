using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

//Act as level builder and accuracy calculator
public class LevelScore : MonoBehaviour
{
    public float playerPrecisionTolerance = 0.3f;//easing out difficulty

    Spline spline;
    float perfectLengthCut = 0;
    Vector3 previousTemp;
    public static float percentageAccuracy;
    Transform cutpoint,testObj;

    // Start is called before the first frame update
    void Start()
    {
        spline = GetComponent<Spline>();
        cutpoint = HorizonStepManager.Inst.cheeseSlicer.transform.GetChild(0).transform;
        testObj = HorizonStepManager.Inst.testObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //if (HorizonStepManager.Inst.cutState == HorizonStepManager.HorizontalCuttingStates.cutting)
        {
            //Vector3 buffer = transform.TransformPoint(HorizonStepManager.Inst.cheeseSlicer.transform.position);
            HorizonStepManager.Inst.testObj.transform.position = HorizonStepManager.Inst.cheeseSlicer.transform.GetChild(0).transform.position;
            Vector3 temp = testObj.localPosition;
            //Debug.Log("Spline projection ="+spline.GetProjectionSample(temp).location);
            //Debug.Log("Distance of knife to spline ="+ Vector3.Distance(spline.GetProjectionSample(temp).location, temp));
            if (Vector3.Distance(spline.GetProjectionSample(temp).location, temp) < playerPrecisionTolerance)
            {
                Debug.Log("within projection distance");
                perfectLengthCut += Vector3.Distance(previousTemp, temp);
            }
            previousTemp = temp;
        }
        //float xPos = 
        percentageAccuracy = Mathf.Clamp((perfectLengthCut / spline.Length) * 100, 0, 100); 

        Debug.Log(percentageAccuracy);

        ////Met minimum accuracy for win
        //if (percentageAccuracy >= GameManager.Inst.GameWinPoint && GameManager.gameOverWinOrLoose == GameManager.GameOverStatus.Loose)
        //{
        //    Debug.Log("In setting win loose =" + GameManager.Inst.GameWinPoint);
        //    GameManager.gameOverWinOrLoose = GameManager.GameOverStatus.Win;
        //    UIManager.Inst.TextWinFeedback();
        //}
    }

    //Generating cut path for each level
    public void LevelInitialiser()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        previousTemp = spline.nodes[0].Position;
        percentageAccuracy = 0;
        for (int i = 0; i < spline.nodes.Count; i++)
        {
            spline.nodes[i].Position = new Vector3(spline.nodes[i].Position.x, Random.Range(-0.65f, 0.65f), spline.nodes[i].Position.z);
            spline.nodes[i].Direction = new Vector3(spline.nodes[i].Direction.x, Random.Range(-0.65f, 0.65f), spline.nodes[i].Direction.z);
        }
    }
}
