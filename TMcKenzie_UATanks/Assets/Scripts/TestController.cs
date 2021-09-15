using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public TankData data;
    public Motor motor;

    // Start is called before the first frame update
    void Start()
    {
        data = this.GetComponent<TankData>();
        motor = this.GetComponent<Motor>();
    }

    // Update is called once per frame
    void Update()
    {
        motor.Move(data.moveRate);
        motor.Turn(data.turnRate);
    }
}
