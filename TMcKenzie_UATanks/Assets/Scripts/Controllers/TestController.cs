using UnityEngine;

[RequireComponent(typeof(TankData))]
public class TestController : MonoBehaviour
{
    public TankData data;
    public Motor motor;

    // Start is called before the first frame update
    void Start()
    {
        CheckForNull();
    }

    void CheckForNull()
    {
        if (data == null)
        {
            data = this.GetComponent<TankData>();
        }
        if (motor == null)
        {
            motor = this.GetComponent<Motor>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
