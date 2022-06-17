using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCutter : MonoBehaviour
{
    public float moveSpeed=10f;

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Move();    
        }
    }

    private void Move()
    {
        //Debug.Log("Joystick values =" + GameManager.Instance.joystick.Vertical);
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime *GameManager.Instance.joystick.Vertical);
        float yPos = (Mathf.Clamp(transform.localPosition.y, 0.1f, 0.9f));
        transform.localPosition = new Vector3(transform.localPosition.x, yPos, transform.localPosition.z);
    }

}
