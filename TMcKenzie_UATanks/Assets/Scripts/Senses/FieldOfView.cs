using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;
    public bool canSeePlayer;
    [SerializeField] float blinkTime;

    public GameObject playerRef;
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    void Start()
    {
        playerRef = GameManager.instance.Player1;
        //StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(blinkTime);

        while(true)
        {
            yield return wait;
            FOVCheck();
        }
    }
    private void Update()
    {
        FOVCheck();
    }

    // Checks to see if the enemy can actually see the player.
    private void FOVCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // If the player is within the sight of the enemy
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                // If something is NOT between the player and enemy (obstructing vision)
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                }
                // Vision is obstructed
                else
                {
                    canSeePlayer = false;
                }
            }
            // Player outside of enemy sight
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }


    }
}
