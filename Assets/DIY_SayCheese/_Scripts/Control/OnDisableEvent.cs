using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDisableEvent : MonoBehaviour
{
    public UnityEvent onDisableEvent;


    void OnDisable()
    {
        onDisableEvent.Invoke();
    }
}
