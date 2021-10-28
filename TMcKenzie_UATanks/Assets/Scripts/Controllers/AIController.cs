using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] public enum PatrolScheme {Stop, Loop, PingPong};
    public PatrolScheme patrolScheme;
    public TankData data;
    public Motor motor;
    public Artillery arty;

    private Transform tf;
    public Transform[] waypoints;
    int currentWaypoint = 0;
    public float closeEnough = 2.0f;
    private bool isPatrolForward = true;
    private bool onLastPoint = false;

    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();

        CheckForNull();
    }

    // Update is called once per frame
    void Update()
    {
        arty.Shoot();


        if (patrolScheme == PatrolScheme.Loop)
        {
            if (motor.RotateTowards(waypoints[currentWaypoint].position, data.GetTurnRate()))
            {
                // Do nothing. ;)
            }
            else
            {
                motor.Move(data.GetForward());
            }

            if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (closeEnough * closeEnough))
            {
                if (currentWaypoint < waypoints.Length - 1)
                {
                    currentWaypoint++;
                }

                // This loops the AI back to the first point.
                else
                {
                    currentWaypoint = 0;
                }
            }
        }
        else if (patrolScheme == PatrolScheme.Stop)
        {
            if (motor.RotateTowards(waypoints[currentWaypoint].position, data.GetTurnRate()))
            {
                // Do nothing. ;)
            }
            else
            {
                if (onLastPoint)
                {
                    if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (closeEnough * closeEnough))
                    {
                        // Do nothing.
                    }
                }
                else
                {
                    motor.Move(data.GetForward());
                }
            }

            if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (closeEnough * closeEnough))
            {
                if (currentWaypoint < waypoints.Length - 1)
                {
                    currentWaypoint++;
                }
                else if (currentWaypoint == waypoints.Length - 1)
                {
                    onLastPoint = true;
                }
            }
        }
        else if (patrolScheme == PatrolScheme.PingPong)
        {

        }
    }

    // Checks to make sure the game object's components are acquired on start.
    void CheckForNull()
    {
        if (data == null)
        {
            data = this.GetComponent<TankData>();
        }
        if (motor == null)
        {
            motor = this.GetComponent<Motor>();
        }
        if (arty == null)
        {
            arty = this.GetComponent<Artillery>();
        }
    }


}
