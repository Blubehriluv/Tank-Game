using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    [SerializeField] float forwardRate = 5;
    [SerializeField] float backwardRate = 3;
    [SerializeField] float turnRate = 180;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetForward()
    {
        return forwardRate;
    }

    public float GetBackward()
    {
        return backwardRate;
    }

    public float GetTurnRate()
    {
        return turnRate;
    }
}
