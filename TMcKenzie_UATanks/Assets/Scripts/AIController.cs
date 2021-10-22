using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public TankData data;
    public Motor motor;
    public Artillery arty;
    // Start is called before the first frame update
    void Start()
    {
        CheckForNull();
    }

    // Update is called once per frame
    void Update()
    {
        arty.Shoot();
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
