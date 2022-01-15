using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenRotate : MonoBehaviour
{
    Transform tf;
    float degreesPerSecond = 20; 
    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tf.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
    }
}
