using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A simple class to store gamplay count through out the session
public class PlayerProfileTracker : MonoBehaviour
{
    public static int gameCount=0;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("PlayerProfile");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

}
