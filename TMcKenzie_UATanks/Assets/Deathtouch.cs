using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathtouch : MonoBehaviour
{
    [SerializeField]int damage = 1000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            // The item hit recieves damage and is communicated to console.
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            Debug.Log(collision.gameObject.name + " ran into an anti-tank barrier.");
        }
    }
}
