using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool willSpawn;
    public static GameManager instance;
    SpawnManager spawnManager;
    RoomGen roomGenerator;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject[] enemies;

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
        roomGenerator = this.gameObject.GetComponent<RoomGen>();
        roomGenerator.GenerateGrid();
        spawnManager = this.gameObject.GetComponent<SpawnManager>();
        spawnManager.InitializeLocations();
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

        spawnManager.SpawnTank(Player1, false);

        for (int i = 0; i < enemies.Length; i++)
        {
            spawnManager.SpawnTank(enemies[i], true);
            i++;
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
            //Instantiate(Player1);
            //spawnManager.SpawnTank(Player1, false);
        }
        else if (!isSinglePlayer)
        {
            // Spawn two players.
        }
    }

    public void SomeoneDied(GameObject Tank)
    {
        spawnManager.SpawnTank(Tank, false);
    }

    public GameObject GivePlayer()
    {
        return Player1;
    }
}
