using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearing : MonoBehaviour
{
    [SerializeField] int hearingRange;
    [SerializeField] SphereCollider collider;
    [SerializeField] private GameObject parentObject;


    // Start is called before the first frame update
    void Start()
    {
        NullChecker();
        collider.radius = hearingRange;
        // Obtains the parent object
        parentObject = gameObject.transform.parent.gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NullChecker()
    {
        if (collider == null)
        {
            collider = gameObject.GetComponent<SphereCollider>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If it's a gunshot
        if (other.gameObject.GetComponent<Projectile>())
        {
            if (other.gameObject.GetComponent<Projectile>().GetParentName() == parentObject.name)
            {
                Debug.Log("I heard myself shoot!");
            }
            else
            {
                Debug.Log("I heard a gunshot!");
            }
        }
        // If there's movement from the player
        else if (other.gameObject.GetComponent<InputController>())
        {
            Debug.Log("I can hear the player!");
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InputController>())
        {
            Debug.Log("I can no longer hear the player");
        }
    }
}
