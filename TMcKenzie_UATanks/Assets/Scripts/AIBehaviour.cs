using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] public enum AttackMode {Chase, Flee, Idle };
    public AttackMode attackMode;
    [SerializeField] enum Personality { Bold, Scared };
    [SerializeField] public Transform chaseTF;
    public float fleeDistance = 1.0f;
    float lengthToBeSafe = 4.0f;
    [Range(4,20)]
    private TankData data;
    private Motor motor;

    // Start is called before the first frame update
    void Start()
    {
        CheckForNull();
    }

    void CheckForNull()
    {
        if (chaseTF == null)
        {
            chaseTF = GameManager.instance.Player1.transform;
        }
        if (data == null)
        {
            data = this.GetComponent<TankData>();
        }
        if (motor == null)
        {
            motor = this.GetComponent<Motor>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attackMode == AttackMode.Idle)
        {
            // Nuffin
        }
        if (attackMode == AttackMode.Chase)
        {
            motor.RotateTowards(chaseTF.position, data.GetTurnRate());
            motor.Move(data.GetForward());
        }

        if (attackMode == AttackMode.Flee)
        {
            Vector3 vectorAwayFromTarget = (-1 * (chaseTF.position - gameObject.transform.position));
            Vector3 fleePosition = (vectorAwayFromTarget.normalized * fleeDistance) + gameObject.transform.position;

            motor.RotateTowards(fleePosition, data.GetTurnRate());
            motor.Move(data.GetForward());
        }
    }
}
