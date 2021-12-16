using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public float fieldOfView = 45.0f;
    private Transform tf;
    // Start is called before the first frame update
    void Start()
    {
        tf = this.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        CanSee(GameManager.instance.Player1);
    }

    public bool CanSee(GameObject target)
    {
        Vector3 agentToTargetVector = target.transform.position - tf.position;
        float angleToTarget = Vector3.Angle(agentToTargetVector, tf.forward);

        if (angleToTarget < fieldOfView)
        {
            Ray rayTotarget = new Ray();

            rayTotarget.origin = tf.position;
            rayTotarget.direction = agentToTargetVector;

            RaycastHit hitInfo;

            if (Physics.Raycast (rayTotarget, out hitInfo, Mathf.Infinity))
            {
                Debug.Log(hitInfo + " was hit");
                if (hitInfo.collider.gameObject == target)
                {
                    Debug.Log("I can see the player");
                    return true;
                }
            }
        }
        Debug.Log("I got nothin");
        return false;
    }
}

