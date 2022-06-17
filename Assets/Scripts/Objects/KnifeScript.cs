using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Knife Object Script
public class KnifeScript : MonoBehaviour
{
    public float speed = 3;
    public float maxAngleTilt = 20;

    float startPos;
    float endPos;
    
    Transform knifeModelObj;

    //setting up knife before gameplay
    public void InitialiseKnife()
    {
        //gameObject.transform.position = GameplayManager.Inst.knifeStartPos.position;
        knifeModelObj = transform.GetComponent<Transform>();
        if (startPos==0)
        {
            startPos = knifeModelObj.position.x;
        }
        else
        {
             knifeModelObj.position =new Vector3( startPos, knifeModelObj.position.y, knifeModelObj.position.z);
        }
        endPos = startPos - 0.6f;
        knifeModelObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    float timeIncrement = 0;
    float lerpValue = 0;
    float tiltValue = 0;
    float margin = 0.7f;

    //Event knife moved for splines to update thier position
    public delegate void OnKinfeMoved();
    public static event OnKinfeMoved KnifeMoved;

    private void Start()
    {
        knifeModelObj = transform.GetComponent<Transform>();
    }

    //Moving knife according to user input
    public void MoveKnife(float joystickValue)
    {
        //Linear Movement

            //Along cutting plane
            timeIncrement += Time.deltaTime;
            //if (transform.position.y >= -margin && transform.position.y <= margin)//limiting
            {
                 transform.Translate((Vector3.right + Vector3.up * joystickValue) * Time.deltaTime * speed, Space.World);
            }
        //else
        //{
        //    transform.Translate((Vector3.right ) * Time.deltaTime * speed, Space.World);
        //    //transform.position=new Vector3(transform.position.x,Mathf.Clamp(transform.position.y, -margin, margin),transform.position.z);
        //}
        //// to and fro motion for cutting feel
        //lerpValue = Mathf.PingPong(timeIncrement * 2, 1);
        //knifeModelObj.position = new Vector3(Mathf.Lerp(startPos, endPos, lerpValue), knifeModelObj.position.y, knifeModelObj.position.z);

        //Blade Tilt depending on vertical movement
        tiltValue = Mathf.Lerp(tiltValue, maxAngleTilt * joystickValue, Time.deltaTime * 4);
        knifeModelObj.rotation = Quaternion.Euler(new Vector3(knifeModelObj.transform.eulerAngles.x, knifeModelObj.transform.eulerAngles.y, tiltValue));

        //Broadcasting
        if (KnifeMoved != null)
        {
           // Debug.Log("In broad casting");
            KnifeMoved.Invoke();
        }
        
    }

    
    //Auto movement to final pos after cut complete
    public void MoveKnifeGameOver()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y,-5.5f), Time.deltaTime * speed);
    }
}
