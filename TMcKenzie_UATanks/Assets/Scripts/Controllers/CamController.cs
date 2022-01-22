using UnityEngine;

public class CamController : MonoBehaviour
{
    float lockPos = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Set screen dimensions

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lockPos, lockPos);
    }

    void SetScreenDimensions()
    {
        //Full Screen mode (1 player mode)

        //Two Player Horizontal

        //Two Player Vertical
    }
}
