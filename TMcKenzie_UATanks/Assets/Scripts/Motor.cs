using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform tf;
    //[SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Moves the tank forward.
    public void Move(float speed)
    {
        Vector3 forwardVector = transform.forward * speed * Time.deltaTime;
        rb.MovePosition(rb.position + (forwardVector));
    }

    // Turns the tank.
    public void Turn(float turnSpeed)
    {
        Vector3 turnVector = Vector3.up * turnSpeed * Time.deltaTime;
        tf.Rotate(turnVector, Space.Self);
    }
}
