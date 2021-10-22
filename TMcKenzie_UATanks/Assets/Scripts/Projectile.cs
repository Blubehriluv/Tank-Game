using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject parentObject;
    [SerializeField] int projDamage;
    [SerializeField] float projSpeed;
    [SerializeField] int projDieTime;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        DestroySelf(projDieTime);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(rb.transform.forward.normalized * projSpeed * Time.deltaTime, ForceMode.Impulse);

    }

    public int GetDamage()
    {
        return projDamage;
    }

    public void SetData(int speed, int damage, int time, GameObject parent)
    {
        projSpeed = speed;
        projDamage = damage;
        projDieTime = time;
        parentObject = parent;
    }

    void DestroySelf(int timeToDestroy)
    {
        Destroy(this.gameObject, timeToDestroy);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == parentObject.name)
        {
            Debug.Log("Stop shooting yourself");
            Debug.Log(collision.gameObject.name + " " + parentObject.name);
        }
        else if (collision.gameObject.GetComponent<Health>())
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(projDamage);
            Debug.Log(parentObject.name + " dealt " + projDamage + " damage to " + collision.gameObject.name);
            DestroySelf(0);
        }
    }
}
