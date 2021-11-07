using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] public enum PatrolScheme {Stop, Loop, PingPong, Idle};
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
        SetTankBehaviors();
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

    /// <summary>
    /// This runs the specified behaviour for the AI this script is placed on.  Choose between Idle, Loop, Stop, and Ping Pong for varying waypoint behavior.
    ///     Idle : The AI will not follow a waypoint route.
    ///     Loop : The AI will run through all waypoints until none remain and then return to the first waypoint to begin again.
    ///     Stop : The AI will run through all waypoints and stop at the final waypoint.
    /// PingPong : The AI will run through all waypoints and upon reaching the final waypoint, they will return back the way they came to the first waypoint and repeat.
    /// 
    /// </summary>
    void SetTankBehaviors()
    {
        switch (patrolScheme)
        {
            case PatrolScheme.Idle:
                Idle();
                break;
            case PatrolScheme.Stop:
                Stop();
                break;
            case PatrolScheme.Loop:
                Loop();
                break;
            case PatrolScheme.PingPong:
                PingPong();
                break;
            default:
                Idle();
                break;
        }
    }

    void Idle()
    {
        // Do nothing B)
    }

    void Stop()
    {
        // Rotate towards the next waypoint.
        if (motor.RotateTowards(waypoints[currentWaypoint].position, data.GetTurnRate()))
        {
            // Do nothing. :P
        }
        // Should we move towards the next destination?
        else
        {
            // If we're on the last point, once the Tank is close enough to the final waypoint, stop moving.
            if (onLastPoint)
            {
                if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (closeEnough * closeEnough))
                {
                    // Do nothing.
                }
            }
            // If we're not on the last point, move to the next destination.
            else
            {
                motor.Move(data.GetForward());
            }
        }

        // If the tank is close enough to the next waypoint
        if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (closeEnough * closeEnough))
        {
            // More waypoints remain, set next destination
            if (currentWaypoint < waypoints.Length - 1)
            {
                currentWaypoint++;
            }
            // No more waypoints remain.
            else if (currentWaypoint == waypoints.Length - 1)
            {
                onLastPoint = true;
            }
        }
    }

    void Loop()
    {
        // Rotate towards the next waypoint.
        if (motor.RotateTowards(waypoints[currentWaypoint].position, data.GetTurnRate()))
        {
            // Do nothing. ;)
        }
        // Move towards destination.
        else
        {
            motor.Move(data.GetForward());
        }

        // If the Tank is close enough to the waypoint
        if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (closeEnough * closeEnough))
        {
            // If there are waypoints left, assign the next waypoint destination
            if (currentWaypoint < waypoints.Length - 1)
            {
                currentWaypoint++;
            }
            // No more waypoints remain. This loops the AI back to the first point.
            else
            {
                currentWaypoint = 0;
            }
        }
    }

    void PingPong()
    {
        if (motor.RotateTowards(waypoints[currentWaypoint].position, data.GetTurnRate()))
        {
            // Do nothing. ;)
        }
        else
        {
            motor.Move(data.GetForward());
        }

        if (isPatrolForward)
        {
            if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (closeEnough * closeEnough))
            {
                if (currentWaypoint < waypoints.Length - 1)
                {
                    currentWaypoint++;
                }

                // This loops the AI back to the first point.
                else
                {
                    isPatrolForward = false;
                }
            }
        }
        else if (!isPatrolForward)
        {
            if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (closeEnough * closeEnough))
            {
                if (currentWaypoint > 0)
                {
                    currentWaypoint--;
                }

                // This loops the AI back to the first point.
                else
                {
                    isPatrolForward = true;
                }
            }
        }
    }
}
