using UnityEngine;

public class ObsAvdAISample : MonoBehaviour
{
    [SerializeField] Transform target;
    private Motor motor;
    private TankData data;
    private Transform tf;
    [SerializeField] int avoidStage = 0;
    [SerializeField] float avoidTime = 2.0f;
    [SerializeField] float exitTime;
    public enum AttackMode { Chase };
    public AttackMode attackMode;

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

    void DoAvoid()
    {
        if (avoidStage == 1)
        {
            motor.Turn(-1 * data.GetTurnRate());

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
    }
}
