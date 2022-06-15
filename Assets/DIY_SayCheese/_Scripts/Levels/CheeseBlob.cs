using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseBlob : MonoBehaviour
{
    float reduceAmount = 0.1f;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cheese")
        {
            float scale = transform.localScale.x;
            scale -= reduceAmount;
            if (scale < 0.5f)
            {
                print("Destroyed");
                Destroy(gameObject);
                return;
            }
            transform.localScale = new Vector3(scale, scale, scale);
            print("Reduced");
        }
    }
}
