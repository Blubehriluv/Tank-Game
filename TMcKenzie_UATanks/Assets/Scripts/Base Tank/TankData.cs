using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    [Header("Score Data")]
    [SerializeField] int hitValue;
    [SerializeField] int killValue;
    [SerializeField] int acquiredPoints;

    [Header("Movement Data")]
    [SerializeField] float forwardRate = 5;
    [SerializeField] float backwardRate = 3;
    [SerializeField] float turnRate = 180;

    [SerializeField] bool isAGhost = false;

    private int playerNumber = 0;

    void Start()
    {
        acquiredPoints = 0;
    }

    void Update()
    {
        
    }

    // Returns the hit point value of this game object.
    public int GetHitPoints()
    {
        return hitValue;
    }

    // Returns the kill point value of this game object.
    public int GetKillPoints()
    {
        return killValue;
    }

    // Adds points to the total score of this game object.
    public void AcquirePoints(int pointsToGain)
    {
        acquiredPoints += pointsToGain;
    }

    // Getter for the rate of forward movement.
    public float GetForward()
    {
        return forwardRate;
    }

    // Getter for the rate of backward movement.
    public float GetBackward()
    {
        return backwardRate;
    }

    // Getter for the rate of turning movement.
    public float GetTurnRate()
    {
        return turnRate;
    }

    public void SetGhost(bool turningIntoAGhost, float ghostTime)
    {
        if (!turningIntoAGhost)
        {
            isAGhost = false;
        }
        else if (turningIntoAGhost)
        {
            isAGhost = true;
            StartCoroutine(Wait(ghostTime));
        }
    }

    public bool GetGhosted()
    {
        return isAGhost;
    }

    IEnumerator Wait(float waitTime)
    {
        Debug.Log("Become a ghost...");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Human again.");
        SetGhost(false, 0);
    }

    public void SetPlayerNumber(int num)
    {
        playerNumber = num;
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }
}
