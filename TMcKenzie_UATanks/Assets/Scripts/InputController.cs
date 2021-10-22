using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] enum InputScheme {WASD, arrowKeys};
    [SerializeField] InputScheme input;
    public TankData data;
    public Motor motor;
    public Artillery arty;

    void Start()
    {
        CheckForNull();
        SelectInput(true);
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
        if (arty == null)
        {
            arty = this.GetComponent<Artillery>();
        }
    }

    public void SelectInput(bool isUsingWASD)
    {
        if (isUsingWASD)
        {
            input = InputScheme.WASD;
        }
        else if (!isUsingWASD)
        {
            input = InputScheme.arrowKeys;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (input)
        {
            case InputScheme.arrowKeys:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    motor.Move(data.GetForward());
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    motor.Move(-data.GetBackward());
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    motor.Turn(-data.GetTurnRate());
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    motor.Turn(data.GetTurnRate());
                }
                if (Input.GetKey(KeyCode.Space))
                {
                }
                break;
            case InputScheme.WASD:
                if (Input.GetKey(KeyCode.W))
                {
                    motor.Move(data.GetForward());
                }
                if (Input.GetKey(KeyCode.S))
                {
                    motor.Move(-data.GetBackward());
                }
                if (Input.GetKey(KeyCode.A))
                {
                    motor.Turn(-data.GetTurnRate());
                }
                if (Input.GetKey(KeyCode.D))
                {
                    motor.Turn(data.GetTurnRate());
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    arty.Shoot();
                }
                break;
        }
    }
}
