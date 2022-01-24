using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] Image[] P1lives;
    [SerializeField]Text playerOneScore;
    [SerializeField]Text playerTwoScore;

    [SerializeField]int currentP1Lives;
    [SerializeField]int currentP2Lives;

    [SerializeField]int playerOnePoints;
    [SerializeField]int playerTwoPoints;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("DELETING EXTRA UI MANAGER.");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetLife();
        currentP1Lives = GameManager.instance.GetCurrentLives(1);
    }

    // Update is called once per frame
    void Update()
    {
        playerOneScore.text = playerOnePoints.ToString();
    }

    void SetLife()
    {
        for (int i = GameManager.instance.GetCurrentLives(1); i < P1lives.Length; i++)
        {
            P1lives[i].gameObject.SetActive(false);
        }
    }

    void GetLife()
    {
    }

    public void LoseALife()
    {
        currentP1Lives--;
        P1lives[currentP1Lives].gameObject.SetActive(false);
        
    }

    void GainALife()
    {
        currentP1Lives++;
        P1lives[currentP1Lives].gameObject.SetActive(true);
    }

    // Adds points to the total score of this game object.
    public void AcquirePoints(int pointsToGain, int playerNumber)
    {
        playerOnePoints += pointsToGain;
    }

}
