using UnityEngine;

public class ObsAvdAISample : MonoBehaviour
{
    [SerializeField] Transform target;
    private Motor motor;
    private TankData data;
    private Transform tf;
    FieldOfView FOV;
    [SerializeField] int avoidStage = 0;
    [SerializeField] float avoidTime = 2.0f;
    [SerializeField] float exitTime;
    public enum AttackMode { Chase };
    public AttackMode attackMode;

    float FOVSide;

    // Start is called before the first frame update
    void Start()
    {
        CheckForNull();
    }

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
        if (tf == null)
        {
            tf = this.gameObject.transform;
        }
        if (target == null)
        {
            target = GameManager.instance.Player1.transform;
        }
        FOV = this.gameObject.GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackMode == AttackMode.Chase)
        {
            if (avoidStage != 0)
            {
                DoAvoid();
            }
            else
            {
                DoChase();
            }
        }
    }

    void SetFieldOfViewAngles(float fieldOfView)
    {
        FOVSide = fieldOfView / 2;

    }

    bool CanMove (float speed)
    {
        RaycastHit hit;
        if (Physics.Raycast (tf.position, tf.forward, out hit, speed))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                return false;
            }
        }

        return true;
    }

    void DoChase()
    {
        motor.RotateTowards(target.position, data.GetTurnRate());

        if (CanMove(data.GetForward()))
        {
            motor.Move(data.GetForward());
        }
        else
        {
            avoidStage = 1;
        }
    }

    /*
    void DoAvoid()
    {
        if (avoidStage == 1)
        {
            RaycastHit hitLeft;
            RaycastHit hitRight;
            RaycastHit forwardToTarget;
            RaycastHit forward;
            Vector3 leftVector = new Vector3(tf.position.x, tf.rotation.y - FOVSide, tf.position.z);
            Vector3 rightVector = new Vector3(tf.position.x, tf.rotation.y + FOVSide, tf.position.z);
            
            if (Physics.Raycast(tf.position, tf.forward, out forward))
            {
                if (Physics.Raycast(tf.position, target.position, out forwardToTarget))
                {
                    float minRange = forward.point.y - FOVSide;
                    float maxRange = forward.point.y + FOVSide;

                    // Checks if the target is within the vision
                    if (forwardToTarget.point.y! > maxRange && forwardToTarget.point.y! < minRange)
                    {
                        // Positive line
                        if (minRange > 0)
                        {
                            if (forwardToTarget.point.y - minRange > maxRange - forwardToTarget.point.y)
                            {
                                motor.Turn(-1 * data.GetTurnRate());
                                // turn right
                            }
                        }
                        // Negative line
                        else if (maxRange < 0)
                        {
                            if (maxRange + forwardToTarget.point.y > forwardToTarget.point.y + -minRange)
                            {
                                motor.Turn(1 * data.GetTurnRate());
                                // go left
                            }
                        }
                        else
                        {
                            motor.Turn(1 * data.GetTurnRate());
                        }
                    }
                    
                    else
                    {
                        float leftAngle, rightAngle;
                        if (Physics.Raycast(tf.position, leftVector, out hitLeft, data.GetForward()))
                        {
                            leftAngle = Vector3.Angle(target.position, leftVector);

                            if (Physics.Raycast(tf.position, rightVector, out hitRight, data.GetForward()))
                            {
                                rightAngle = Vector3.Angle(target.position, rightVector);

                                if (rightAngle > leftAngle)
                                {
                                    motor.Turn(1 * data.GetTurnRate());
                                }
                                else
                                {
                                    motor.Turn(-1 * data.GetTurnRate());
                                }
                            }
                            motor.Turn(-1 * data.GetTurnRate());
                        }
                        else
                        {
                            motor.Turn(-1 * data.GetTurnRate());
                            // ADD A KICKER OUT TO CHANGE THE STAGE NUMBER PLEASE
                        }
                        motor.Turn(-1 * data.GetTurnRate());
                        
                    }   
                }
            }

            if (CanMove(data.GetForward()))
            {
                avoidStage = 2;
                exitTime = avoidTime;
            }
        }
        else if (avoidStage == 2)
        {
            if (CanMove(data.GetForward()))
            {
                exitTime -= Time.deltaTime;
                motor.Move(data.GetForward());

                if (exitTime <= 0)
                {
                    avoidStage = 0;
                }
            }
            else
            {
                avoidStage = 1;
            }
        }
    }*/

    void DoAvoid()
    {
        if (avoidStage == 1)
        {
            // Rotate left
            motor.Turn(-1 * data.GetTurnRate());

            // If I can now move forward, move to stage 2!
            if (CanMove(data.GetForward()))
            {
                avoidStage = 2;

                // Set the number of seconds we will stay in Stage2
                exitTime = avoidTime;
            }

            // Otherwise, we'll do this again next turn!
        }
        else if (avoidStage == 2)
        {
            // if we can move forward, do so
            if (CanMove(data.GetForward()))
            {
                // Subtract from our timer and move
                exitTime -= Time.deltaTime;
                motor.Move(data.GetForward());

                // If we have moved long enough, return to chase mode
                if (exitTime <= 0)
                {
                    avoidStage = 0;
                }
            }
            else
            {
                // Otherwise, we can't move forward, so back to stage 1
                avoidStage = 1;
            }
        }
    }
}
