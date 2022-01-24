using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool willSpawn;
    public static GameManager instance;

    SpawnManager spawnManager;
    RoomGen roomGenerator;
    public GameObject pauseMenu;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject[] enemies;
    [SerializeField] int maxLives;
    [SerializeField] int playerOneCurrentLives;
    [SerializeField] int playerTwoCurrentLives;
    //public int MOTD = 428;

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
        }

        playerOneCurrentLives = maxLives;
        playerTwoCurrentLives = maxLives;

        pauseMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
            }
            else
            {
                pauseMenu.SetActive(true);
            }
        }

        if (playerOneCurrentLives == 0 || playerTwoCurrentLives == 0) {
            Debug.Log("GAME OVER");
        }
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
        Debug.Log("Attemping to spawn: " + Tank.name);
    }

    public void SetCurrentLives(int playerNumber)
    {
        if (playerNumber == 1 || playerNumber == 0)
        {
            Debug.Log("Manager is diogn it's job");
            UIManager.instance.LoseALife();
            playerOneCurrentLives--;
        }
        else if (playerNumber == 2)
        {
            playerTwoCurrentLives--;
        }
    }

    public int GetCurrentLives(int playerNumber)
    {
        if (playerNumber == 1 || playerNumber == 0)
        {
            return playerOneCurrentLives;
        }
        else if (playerNumber == 2)
        {
            return playerTwoCurrentLives;
        }
        else
        {
            return 0;
        }
    }

    public GameObject GivePlayer()
    {
        return Player1;
    }
}
