using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timerDelay = 5.0f;
    [SerializeField] float timeUntilNextEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilNextEvent -= Time.deltaTime;
        if(timeUntilNextEvent <= 0)
        {
            Debug.Log("Timer!");
            timeUntilNextEvent = timerDelay;
        }
    }
}
