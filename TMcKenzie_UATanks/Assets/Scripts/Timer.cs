using UnityEngine;

public class Timer : MonoBehaviour
{
    /// <summary>
    /// A timer class that gives an object a timer for use.
    /// </summary>

    [SerializeField] float timerDelay = 5.0f;
    [SerializeField] float timeUntilNextEvent;
    [SerializeField] string timeIsUpMessage;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // This runs the timer with a custom message.
    public void CallTimer(string message)
    {
        if (message == "")
        {
            message = "Timer is up on " + this.gameObject.name;
        }

        timeUntilNextEvent -= Time.deltaTime;
        if (timeUntilNextEvent <= 0)
        {
            Debug.Log(message);
            timeUntilNextEvent = timerDelay;
        }
    }
}
