using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonSleepMode : MonoBehaviour
{
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Update()
    {
        
    }
}
