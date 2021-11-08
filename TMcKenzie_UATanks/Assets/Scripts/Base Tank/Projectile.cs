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

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        DestroySelf(projDieTime);
    }

    void Update()
    {
        // Once spawned, the projectile has an 'explosive' force added.
        rb.AddForce(rb.transform.forward.normalized * projSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    // Get the damage of this projectile.
    public int GetDamage()
    {
        return projDamage;
    }

    // Set the data needed for this projectile.
    public void SetData(int speed, int damage, int time, GameObject parent)
    {
        projSpeed = speed;
        projDamage = damage;
        projDieTime = time;
        parentObject = parent;
    }

    // The projectile is destroyed after a given time.
    void DestroySelf(int timeToDestroy)
    {
        Destroy(this.gameObject, timeToDestroy);
    }

    public string GetParentName()
    {
        return parentObject.name;
    }

    // The bullet runs into something.
    private void OnCollisionEnter(Collision collision)
    {
        // If the bullet runs into the object that shot it, nothing will happen.
        if (collision.gameObject.name == parentObject.name)
        {
            Debug.Log("Stop shooting yourself");
            Debug.Log(collision.gameObject.name + " " + parentObject.name);
        }
        // If the bullet runs into an object with a health component, continue.
        else if (collision.gameObject.GetComponent<Health>())
        {
            // If the collided object also has a TankData component, points should be awarded.
            if (collision.gameObject.GetComponent<TankData>())
            {
                // Calculates if the tank hit will die on impact.
                float helperVariable = collision.gameObject.GetComponent<Health>().GetHealth() - projDamage;

                // If the tank will die, the shooter recieves the kill value points.
                if (helperVariable <= 0)
                {
                    parentObject.GetComponent<TankData>().AcquirePoints(collision.gameObject.GetComponent<TankData>().GetKillPoints());
                }
                // If the tank will not die, the shooter recieves the hit value points.
                else
                {
                    parentObject.GetComponent<TankData>().AcquirePoints(collision.gameObject.GetComponent<TankData>().GetHitPoints());
                }
            }
            // The item hit recieves damage and is communicated to console.
            collision.gameObject.GetComponent<Health>().TakeDamage(projDamage);
            Debug.Log(parentObject.name + " dealt " + projDamage + " damage to " + collision.gameObject.name);

            // The projectile dies immediately upon impact.
            DestroySelf(0);
        }
    }
}
