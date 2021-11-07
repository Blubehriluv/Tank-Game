using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool willSpawn;
    public static GameManager instance;
    public GameObject Player1;
    public GameObject Player2;

    public enum PlayMode { Single, Multi };
    public PlayMode playMode;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("DELETING EXTRA GAME MANAGER.");
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (willSpawn)
        {
            switch (playMode)
            {
                case PlayMode.Single:
                    SpawnPlayers(true);
                    break;
                case PlayMode.Multi:
                    SpawnPlayers(false);
                    break;
                default:
                    playMode = PlayMode.Single;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPlayers(bool isSinglePlayer)
    {
        if (isSinglePlayer)
        {
            Instantiate(Player1);
            
        }
        else if (!isSinglePlayer)
        {
            // Spawn two players.
        }
    }
}
