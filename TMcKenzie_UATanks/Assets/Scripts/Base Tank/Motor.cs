using UnityEngine;

public class Motor : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform tf;

    public TankData data;

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
        Vector3 forwardVector = tf.forward * speed * Time.deltaTime;
        rb.MovePosition(rb.position + (forwardVector));
    }

    // Turns the tank.
    public void Turn(float turnSpeed)
    {
        Vector3 turnVector = Vector3.up * turnSpeed * Time.deltaTime;
        tf.Rotate(turnVector, Space.Self);
    }

    public bool RotateTowards(Vector3 target, float speed)
    {
        Vector3 vectorToTarget = target - tf.position;
        Quaternion targetRot = Quaternion.LookRotation(vectorToTarget);

        if (targetRot == tf.rotation)
        {
            return false;
        }
        else
        {
            tf.rotation = Quaternion.RotateTowards(tf.rotation, targetRot, speed * Time.deltaTime);
            return true;
        }
    }
}
