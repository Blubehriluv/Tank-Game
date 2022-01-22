using UnityEngine;

public class TUT_FSM : MonoBehaviour
{
    [SerializeField] TankData tankData;
    [SerializeField] Health healthData;
    [SerializeField] Transform tf;
    [SerializeField] Transform target;
    private Motor motor;
    private TankData data;

    public enum AIState { Chase, ChaseAndFire, CheckForFlee, Flee, Rest };
    public AIState aiState = AIState.Chase;
    public float stateEnterTime;
    public float aiSenseRadius;
    public float restingHealRate;

    [SerializeField] int avoidStage = 0;
    [SerializeField] float avoidTime = 2.0f;
    [SerializeField] float exitTime;

    // Start is called before the first frame update
    void Start()
    {
        CheckForNull();

    }

    void CheckForNull()
    {
        if (data == null)
        {
            tankData = this.GetComponent<TankData>();
        }
        if (healthData == null)
        {
            healthData = this.GetComponent<Health>();
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
        SetTankBehaviors();
    }

    public void SetTankBehaviors()
    {
        Vector3 distance = target.transform.position - tf.position;
        switch (aiState)
        {
            case AIState.Chase:
                if (avoidStage != 0)
                {
                    DoAvoid();
                }
                else
                {
                    DoChase();
                }

                if (healthData.GetHealth() < healthData.GetMaxHealth() * .5f)
                {
                    ChangeState(AIState.CheckForFlee);
                }

                break;
            case AIState.ChaseAndFire:
                break;
            case AIState.Flee:
                break;
            case AIState.CheckForFlee:
                break;
            case AIState.Rest:
                break;
            default:
                break;
        }
    }

    public void DoRest()
    {
        float healthAddition = healthData.GetHealth();
        while (healthData.GetHealth() != healthData.GetMaxHealth())
        {
            healthData.SetCurrentHealth(healthAddition += restingHealRate * Time.deltaTime);
        }
    }

    public void ChangeState(AIState newState)
    {
        aiState = newState;
        stateEnterTime = Time.time;
    }

    bool CanMove(float speed)
    {
        RaycastHit hit;
        if (Physics.Raycast(tf.position, tf.forward, out hit, speed))
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
