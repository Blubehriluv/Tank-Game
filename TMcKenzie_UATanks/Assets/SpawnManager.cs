using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPoints;
    public List<Transform> spawnLocations;
    public List<Transform> openLocations;
    [SerializeField] int enemiesToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        spawnLocations = new List<Transform>();
        
        InitializeLocations();
        CheckLocations();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeLocations()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnLocations.Add(spawnPoints[i].GetComponent<Transform>().transform);

        }
    }

    public void SpawnTank(GameObject Tank, bool isMultiple)
    {
        CheckLocations();
        if (!isMultiple)
        {
            int tempIndex = RandomNumber(0, openLocations.Count);
            Instantiate(Tank, openLocations[tempIndex].position, openLocations[tempIndex].rotation);
        }
        else
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                int tempIndex = RandomNumber(0, openLocations.Count);
                Instantiate(GameManager.instance.enemies[RandomNumber(0, GameManager.instance.enemies.Length)], openLocations[tempIndex].position, openLocations[tempIndex].rotation );
            }
        }
    }

    void CheckLocations()
    {
        openLocations = new List<Transform>();
        Debug.Log("we in here");
        Debug.Log($"spawn location count is {spawnLocations.Count}");
        for (int i = 0; i < spawnLocations.Count; i++)
        {
            if (spawnLocations[i].gameObject.GetComponent<Spawn>().TypeOfSpawn())
            {
                Debug.Log($"spawnLocation at index {i} returns true type of spawn.");
                Debug.Log($"spawnLocation at index {i} has an occupation check of: {spawnLocations[i].GetComponent<Spawn>().OccupationCheck()}");
                if (!spawnLocations[i].GetComponent<Spawn>().OccupationCheck())
                {
                    Debug.Log($"spawnLocations at index {i} is not occupied. Adding to open locations.");
                    
                    openLocations.Add(spawnPoints[i].GetComponent<Transform>());
                    //continue;
                }
            }
        }
    }

    int RandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }
}
