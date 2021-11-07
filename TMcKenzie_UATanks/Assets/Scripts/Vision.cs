using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    [SerializeField] Transform eyes;
    [SerializeField] Transform player;
    [SerializeField] Vector3 distanceToBeSeen;
    public float rotationSpeed;
    public float distance;
    public Gradient redColor;
    public Gradient greenColor;
    public Gradient yellowColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Draws raycast for vision forward
        RaycastHit2D hitInfo = Physics2D.Raycast(eyes.position, eyes.position + eyes.forward, distance);
        Debug.DrawLine(eyes.position, eyes.position + eyes.forward * distance, Color.green);

        // Draws raycast towards player location
        //RaycastHit2D playerInfo = Physics2D.Raycast(player.position, eyes.position);
        //Debug.DrawLine(eyes.position, player.position);

        /*
        if (playerInfo.collider != null)
        {
            Debug.Log("okay hitting nothing");
        }
        else
        {
            // Returns the player location.
            playerInfo.point = player.position + eyes.forward * distance;
            //Debug.Log(playerInfo.point);
        }*/


        if (hitInfo.collider != null)
        {
            // bad programming practice haha
        }
        else
        {
            hitInfo.point = ((eyes.position + eyes.forward) + eyes.forward);
            Debug.Log(hitInfo.point);
        }

        //distanceToBeSeen = playerContact - visionContact;

        /*
        if (distanceToBeSeen >= 3)
        {
            Debug.Log("player is close");
        }*/
    }
}

