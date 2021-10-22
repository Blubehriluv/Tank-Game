using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]int projDamage;
    [SerializeField]float projSpeed;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        DestroySelf(2);
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

    public void SetDamageAndSpeed(int speed, int damage)
    {
        projSpeed = speed;
        projDamage = damage;
    }

    void DestroySelf(int timeToDestroy)
    {
        Destroy(this.gameObject, timeToDestroy);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == this.gameObject)
        {
            Debug.Log("Stop shooting yourself");
        }
        else if (collision.gameObject.GetComponent<Health>())
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(projDamage);
            Debug.Log("Dealt " + projDamage + " damage to " + collision.gameObject.name);
            DestroySelf(0);
        }
    }
}
